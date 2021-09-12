using RetSim.Events;

namespace RetSim.EventQueues
{
    public class SetQueue : SortedSet<Event>, IEventQueue
    {
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
    }
}