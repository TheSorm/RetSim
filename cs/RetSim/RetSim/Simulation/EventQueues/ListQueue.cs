using RetSim.Events;

namespace RetSim.EventQueues
{
    public class ListQueue : List<Event>, IEventQueue
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
            return this[0];
        }

        public Event RemoveNext()
        {
            Event next = this[0];
            RemoveAt(0);
            return next;
        }

        public void EnsureSorting()
        {
            Sort();
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