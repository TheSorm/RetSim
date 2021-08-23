using System.Collections.Generic;

namespace RetSim.Events
{
    public class GCDEndEvent : Event
    {
        private const int GCDEndEventPriority = 0;
        public GCDEndEvent(int expirationTime, Player player) : base(expirationTime, GCDEndEventPriority, player)
        {
        }

        public override ProcMask Execute(int time, List<Event> resultingEvents)
        {
            player.RemoveGCD();
            return ProcMask.None;
        }

        public override string ToString()
        {
            return "GCD ends";
        }
    }
}
