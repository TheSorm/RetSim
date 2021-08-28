using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class EventQueue : List<Event>
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

        public void RemoveNext()
        {
            RemoveAt(0);
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }
    }
}