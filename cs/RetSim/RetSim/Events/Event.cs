using System;
using System.Collections.Generic;

namespace RetSim
{
    abstract public class Event : IComparable<Event>
    {
        protected Player player;
        public int ExpirationTime { get; private set; }

        protected Event(int expirationTime, Player player)
        {
            this.ExpirationTime = expirationTime;
            this.player = player;
        }
        public abstract int Execute(List<Event> resultingEvents, int time);

        public int CompareTo(Event other)
        {
            return ExpirationTime.CompareTo(other.ExpirationTime);
        }
    }
}