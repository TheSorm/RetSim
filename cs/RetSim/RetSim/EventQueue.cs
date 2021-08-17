using System;
using System.Collections.Generic;

namespace RetSim
{
    internal class EventQueue
    {
        List<Event> eventQueue = new();
        internal EventQueue()
        {
        }

        internal void Push(Event e)
        {
            eventQueue.Add(e);
        }

        internal void Push(List<Event> resultingEvents)
        {
            eventQueue.AddRange(resultingEvents);
        }

        internal void Sort()
        {
            eventQueue.Sort(delegate (Event x, Event y)
            {
                if (x == null && y == null) return 0;
                else if (x == null) return -1;
                else if (y == null) return 1;
                else return x.ExpirationTime > y.ExpirationTime ? -1 : 1;
            });
        }

        internal Event GetNext()
        {
            return eventQueue[0];
        }

        internal void RemoveNext()
        {
            eventQueue.RemoveAt(0);
        }

        internal bool Empty()
        {
            return eventQueue.Count == 0;
        }

    }
}
