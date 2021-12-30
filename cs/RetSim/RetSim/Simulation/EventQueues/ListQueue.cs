using RetSim.Simulation.Events;

namespace RetSim.Simulation.EventQueues
{
    public class ListQueue : List<Event>, IEventQueue
    {
        private bool dirty = false;

        public new void Add(Event e)
        {
            if (e != null)
            {
                base.Add(e);
                dirty = true;
            }
        }

        public void AddRange(List<Event> events)
        {
            foreach (Event e in events)
            {
                Add(e);
            }
        }

        public Event GetNext()
        {
            return this[Count - 1];
        }

        public Event RemoveNext()
        {
            Event next = this[Count - 1];
            RemoveAt(Count - 1);
            return next;
        }

        public void EnsureSorting()
        {
            if (dirty)
            {
                Sort((a, b) => b.CompareTo(a));
                dirty = false;
            }
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void UpdateRemove(Event e)
        {
        }

        public void UpdateAdd(Event e)
        {
        }
    }
}