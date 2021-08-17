using System;
using System.Collections.Generic;

namespace RetSim
{
    internal class EventQueue : List<Event>
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

        internal Event GetNext()
        {
            return this[0];
        }

        internal void RemoveNext()
        {
            RemoveAt(0);
        }

        internal bool Empty()
        {
            return Count == 0;
        }
    }
}
