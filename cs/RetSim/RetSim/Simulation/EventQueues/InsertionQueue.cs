using RetSim.Simulation.Events;

namespace RetSim.Simulation.EventQueues
{
    public class InsertionQueue : List<Event>, IEventQueue
    {
        public new void Add(Event e)
        {
            if (e != null)
            {
                int low = 0;
                int high = Count;

                while (low < high)
                {
                    var mid = (low + high) >> 1;
                    if (this[mid].CompareTo(e) >= 0)
                        low = mid + 1;
                    else
                        high = mid;
                }
                this.Insert(low, e);
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

        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void UpdateRemove(Event e)
        {
            this.Remove(e);
        }

        public void UpdateAdd(Event e)
        {
            this.Add(e);
        }
    }
}