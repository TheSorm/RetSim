using System.Collections.Generic;

namespace RetSim
{
    abstract internal class Event
    {
        protected Player player;
        public int ExpirationTime { get; private set; }

        protected Event(int expirationTime, Player player)
        {
            this.ExpirationTime = expirationTime;
            this.player = player;
        }
        internal abstract int Execute(List<Event> resultingEvents, int time);
    }
}