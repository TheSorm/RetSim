using System;

namespace RetSim.Events
{
    public abstract class Event : IComparable<Event>
    {
        public int Timestamp { get; set; }
        public int Priority { get; init; }

        protected FightSimulation Fight { get; init; }

        protected Event(FightSimulation fight, int timestamp, int priority = 0)
        {
            Fight = fight;

            Timestamp = timestamp;
            Priority = priority;
        }

        public abstract ProcMask Execute(object arguments = null);

        public int CompareTo(Event other)
        {
            return Timestamp.CompareTo(other.Timestamp) == 0 ? Priority.CompareTo(other.Priority) : Timestamp.CompareTo(other.Timestamp);
        }
    }
}