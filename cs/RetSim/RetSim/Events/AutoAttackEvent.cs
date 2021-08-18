using System.Collections.Generic;

namespace RetSim.Events
{
    public class AutoAttackEvent : Event
    {
        public AutoAttackEvent(int expirationTime, Player player) : base(expirationTime, player)
        {
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            return player.MeleeAttack(time, resultingEvents);
        }

        public override string ToString()
        {
            return "Auto attack lands";
        }
    }
}
