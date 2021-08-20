using System.Collections.Generic;

namespace RetSim.Events
{
    public class GCDEndEvent : Event
    {
        private const int GCDEndEventPriority = 0;
        public GCDEndEvent(int expirationTime, Player player) : base(expirationTime, GCDEndEventPriority, player)
        {
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            player.RemoveGCD();
            return 0;
        }

        public override string ToString()
        {
            return "GCD ends";
        }
    }
}
