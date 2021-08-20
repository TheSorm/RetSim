
using System.Collections.Generic;

namespace RetSim.Events
{
    public class CooldownEndEvent : Event
    {
        private const int GCDEndEventPriority = 2;
        private readonly int spellId;

        public CooldownEndEvent(int expirationTime, Player player, int spellId) : base(expirationTime, GCDEndEventPriority, player)
        {
            this.spellId = spellId;
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            player.Spellbook.EndCooldown(player.Spellbook.ByID[spellId]);

            return 0;
        }

        public override string ToString()
        {
            return "Cooldown of " + Spells.ByID[spellId].Name + " ends";
        }
    }
}
