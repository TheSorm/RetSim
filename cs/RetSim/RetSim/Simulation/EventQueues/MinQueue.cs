using RetSim.Simulation.Events;

namespace RetSim.Simulation.EventQueues
{
    public class MinQueue : List<Event>, IEventQueue
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
            if (Count > 0)
            {
                Event min = this[0];
                int minPos = 0;
                for (int i = 1; i < Count; i++)
                {
                    if (min.CompareTo(this[i]) > 0)
                    {
                        min = this[i];
                        minPos = i;
                    }
                }

                Event tmp = this[Count - 1];
                this[Count - 1] = this[minPos];
                this[minPos] = tmp;
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