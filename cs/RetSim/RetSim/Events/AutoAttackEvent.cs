using System.Collections.Generic;

namespace RetSim.Events
{
    public class AutoAttackEvent : Event
    {
        private const int AutoAttackEventPriority = 4;

        public AutoAttackEvent(int expirationTime, Player player) : base(expirationTime, AutoAttackEventPriority, player)
        {
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            int damage = Formulas.Damage.Melee(341, 513, 3.6f, 0, 2000, 1f);

            resultingEvents.Add(new DamageEvent(ExpirationTime, player, "Melee", "Hit", damage, School.Physical));
            return player.MeleeAttack(time, resultingEvents);
        }

        public override string ToString()
        {
            return "Auto attack lands";
        }
    }
}
