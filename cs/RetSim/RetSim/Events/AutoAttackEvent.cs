using System.Collections.Generic;

namespace RetSim.Events
{
    public class AutoAttackEvent : Event
    {
        private const int AutoAttackEventPriority = 4;

        public AutoAttackEvent(int expirationTime, Player player) : base(expirationTime, AutoAttackEventPriority, player)
        {
        }

        public override ProcMask Execute(int time, List<Event> resultingEvents)
        {
            return player.MeleeAttack(time, resultingEvents);
        }

        public override string ToString()
        {
            return "Auto attack lands";
        }
    }
}
