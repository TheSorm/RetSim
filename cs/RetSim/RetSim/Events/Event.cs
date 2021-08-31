using System;

namespace RetSim.Events
{
    public abstract class Event : IComparable<Event>
    {
        private int _TimeStemp;
        public int Timestamp { get { return _TimeStemp; } set { UpdateTimeStemp(value); } }
        public int Priority { get; init; }

        protected FightSimulation Fight { get; init; }

        protected Event(FightSimulation fight, int timestamp, int priority = 0)
        {
            Fight = fight;

            _TimeStemp = timestamp;
            Priority = priority;
        }

        public abstract ProcMask Execute(object arguments = null);

        public void SetTimeStemp(int value)
        {
            _TimeStemp = value;
        }

        private void UpdateTimeStemp(int value)
        {
            Fight.Queue.UpdateRemove(this);
            _TimeStemp = value;
            Fight.Queue.UpdateAdd(this);
        }


        public int CompareTo(Event other)
        {
            return Timestamp.CompareTo(other.Timestamp) == 0 ? Priority.CompareTo(other.Priority) : Timestamp.CompareTo(other.Timestamp);
        }
    }
}