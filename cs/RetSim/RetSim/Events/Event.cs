using System;
using System.Collections.Generic;

namespace RetSim.Events
{
    public abstract class Event : IComparable<Event>
    {
        protected Player player;
        public int ExpirationTime { get; set; }
        public int Priority { get; init; }

        protected Event(int expirationTime, int priority, Player player)
        {
            ExpirationTime = expirationTime;
            Priority = priority;
            this.player = player;
        }
        public abstract ProcMask Execute(int time, List<Event> resultingEvents);

        public int CompareTo(Event other)
        {
            return ExpirationTime.CompareTo(other.ExpirationTime) == 0 ? Priority.CompareTo(other.Priority) : ExpirationTime.CompareTo(other.ExpirationTime);
        }
    }
}