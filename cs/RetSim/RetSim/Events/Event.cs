using System;
using System.Collections.Generic;

namespace RetSim.Events
{
    abstract public class Event : IComparable<Event>
    {
        protected Player player;
        public int ExpirationTime { get; set; }

        protected Event(int expirationTime, Player player)
        {
            ExpirationTime = expirationTime;
            this.player = player;
        }
        public abstract int Execute(int time, List<Event> resultingEvents);

        public int CompareTo(Event other)
        {
            return ExpirationTime.CompareTo(other.ExpirationTime);
        }
    }
}