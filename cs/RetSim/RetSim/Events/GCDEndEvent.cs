using System.Collections.Generic;

namespace RetSim.Events
{
    class GCDEndEvent : Event
    {
        public GCDEndEvent(int expirationTime, Player player) : base(expirationTime, player)
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
