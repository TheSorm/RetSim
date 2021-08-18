using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class EventQueue : List<Event>
    {
        new public Event this[int index]
        {
            get
            {
                if (index > Count)
                    return null;

                else
                    return base[index];
            }
        }

        public Event GetNext()
        {
            return this[0];
        }

        public void RemoveNext()
        {
            RemoveAt(0);
        }

        public bool Empty()
        {
            return Count == 0;
        }
    }
}