using RetSim.Simulation.Events;
using System.Linq;

namespace RetSim.Simulation.EventQueues
{
    public class SetQueue : SortedSet<Event>, IEventQueue
    {
        public SetQueue() : base(new EventComparer())
        { }

        public new void Add(Event e)
        {
            if (e != null)
                base.Add(e);
        }

        public void AddRange(List<Event> events)
        {
            foreach (Event e in events)
                Add(e);
        }

        public Event GetNext()
        {
            return this.First();
        }

        public Event RemoveNext()
        {
            Event next = this.First();
            Remove(next);
            return next;
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
            Remove(e);
        }

        public void UpdateAdd(Event e)
        {
            Add(e);
        }
        private class EventComparer : IComparer<Event>
        {
            public int Compare(Event x, Event y)
            {
                int result = x.CompareTo(y);
                if (result == 0) 
                    return x.GetHashCode().CompareTo(y.GetHashCode());
                return result;
            }
        }
    }
}