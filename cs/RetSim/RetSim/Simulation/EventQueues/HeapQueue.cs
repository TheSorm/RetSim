using RetSim.Events;
using System;
using System.Collections.Generic;

namespace RetSim.EventQueues
{
    public class HeapQueue : IEventQueue
    {
        public void Add(Event e)
        {
            if (e != null)
            {
                Node node = new(e);
                EventToNode.Add(e, node);
                Insert(node);
            }
        }

        public void AddRange(List<Event> events)
        {
            foreach (Event e in events)
                Add(e);
        }

        public Event GetNext()
        {
            return minNode.Data;
        }

        public Event RemoveNext()
        {
            Event e = RemoveMin().Data;
            EventToNode.Remove(e);
            return e;
        }

        public void EnsureSorting()
        {
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void UpdateRemove(Event e)
        {
            Delete(EventToNode[e]);
            EventToNode.Remove(e);
        }

        public void UpdateAdd(Event e)
        {
            Add(e);
        }

        private readonly Dictionary<Event, Node> EventToNode = new();

        private class Node
        {
            public Node(Event e)
            {
                Right = this;
                Left = this;
                Data = e;
            }

            public Event Data { get; }
            internal Node Child { get; set; }
            internal Node Left { get; set; }
            internal Node Parent { get; set; }
            internal Node Right { get; set; }
            internal bool Mark { get; set; }
            internal int Degree { get; set; }
        }


        private Node minNode;

        public int Count { get; private set; }

        public void Clear()
        {
            minNode = null;
            Count = 0;
        }

        private void DecreaseKey(Node x, int k)
        {
            if (k.CompareTo(x.Data.Timestamp) > 0)
            {
                return;
            }

            x.Data.SetTimeStemp(k);

            Node y = x.Parent;

            if (y != null && x.Data.CompareTo(y.Data) < 0)
            {
                Cut(x, y);
                CascadingCut(y);
            }

            if (x.Data.CompareTo(minNode.Data) < 0)
            {
                minNode = x;
            }
        }

        private void Delete(Node x)
        {
            DecreaseKey(x, int.MinValue);
            RemoveMin();
        }

        private void Insert(Node node)
        {
            if (minNode != null)
            {
                node.Left = minNode;
                node.Right = minNode.Right;
                minNode.Right = node;
                node.Right.Left = node;

                if (node.Data.CompareTo(minNode.Data) < 0)
                {
                    minNode = node;
                }
            }
            else
            {
                minNode = node;
            }

            Count++;
        }

        private Node RemoveMin()
        {
            Node currentMin = minNode;

            if (currentMin != null)
            {
                int numKids = currentMin.Degree;
                Node oldMinChild = currentMin.Child;

                while (numKids > 0)
                {
                    Node tempRight = oldMinChild.Right;

                    oldMinChild.Left.Right = oldMinChild.Right;
                    oldMinChild.Right.Left = oldMinChild.Left;

                    oldMinChild.Left = minNode;
                    oldMinChild.Right = minNode.Right;
                    minNode.Right = oldMinChild;
                    oldMinChild.Right.Left = oldMinChild;

                    oldMinChild.Parent = null;
                    oldMinChild = tempRight;
                    numKids--;
                }

                currentMin.Left.Right = currentMin.Right;
                currentMin.Right.Left = currentMin.Left;

                if (currentMin == currentMin.Right)
                {
                    minNode = null;
                }
                else
                {
                    minNode = currentMin.Right;
                    Consolidate();
                }

                Count--;
            }

            return currentMin;
        }

        private void CascadingCut(Node y)
        {
            Node z = y.Parent;

            if (z != null)
            {
                if (!y.Mark)
                {
                    y.Mark = true;
                }
                else
                {
                    Cut(y, z);
                    CascadingCut(z);
                }
            }
        }

        private void Consolidate()
        {
            int arraySize = (int)Math.Floor(Math.Log(Count) * (1.0 / Math.Log((1.0 + Math.Sqrt(5.0)) / 2.0))) + 1;

            var array = new List<Node>(arraySize);

            for (var i = 0; i < arraySize; i++)
            {
                array.Add(null);
            }

            var numRoots = 0;
            Node x = minNode;

            if (x != null)
            {
                numRoots++;
                x = x.Right;

                while (x != minNode)
                {
                    numRoots++;
                    x = x.Right;
                }
            }

            while (numRoots > 0)
            {
                int d = x.Degree;
                Node next = x.Right;

                while (true)
                {
                    Node y = array[d];
                    if (y == null)
                    {
                        break;
                    }

                    if (x.Data.CompareTo(y.Data) > 0)
                    {
                        Node temp = y;
                        y = x;
                        x = temp;
                    }

                    Link(y, x);
                    array[d] = null;
                    d++;
                }

                array[d] = x;

                x = next;
                numRoots--;
            }

            minNode = null;

            for (var i = 0; i < arraySize; i++)
            {
                Node y = array[i];
                if (y == null)
                {
                    continue;
                }

                if (minNode != null)
                {
                    y.Left.Right = y.Right;
                    y.Right.Left = y.Left;

                    y.Left = minNode;
                    y.Right = minNode.Right;
                    minNode.Right = y;
                    y.Right.Left = y;

                    if (y.Data.CompareTo(minNode.Data) < 0)
                    {
                        minNode = y;
                    }
                }
                else
                {
                    minNode = y;
                }
            }
        }

        private void Cut(Node x, Node y)
        {
            x.Left.Right = x.Right;
            x.Right.Left = x.Left;
            y.Degree--;

            if (y.Child == x)
            {
                y.Child = x.Right;
            }

            if (y.Degree == 0)
            {
                y.Child = null;
            }

            x.Left = minNode;
            x.Right = minNode.Right;
            minNode.Right = x;
            x.Right.Left = x;

            x.Parent = null;
            x.Mark = false;
        }

        private static void Link(Node newChild, Node newParent)
        {
            newChild.Left.Right = newChild.Right;
            newChild.Right.Left = newChild.Left;

            newChild.Parent = newParent;

            if (newParent.Child == null)
            {
                newParent.Child = newChild;
                newChild.Right = newChild;
                newChild.Left = newChild;
            }
            else
            {
                newChild.Left = newParent.Child;
                newChild.Right = newParent.Child.Right;
                newParent.Child.Right = newChild;
                newChild.Right.Left = newChild;
            }

            newParent.Degree++;
            newChild.Mark = false;
        }
    }
}
