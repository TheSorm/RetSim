
using System.Collections.Generic;

namespace RetSim.Events
{
    public class CooldownEndEvent : Event
    {
        private const int GCDEndEventPriority = 2;
        private readonly Spell spell;

        public CooldownEndEvent(int expirationTime, Player player, Spell spell) : base(expirationTime, GCDEndEventPriority, player)
        {
            this.spell = spell;
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            player.Spellbook.EndCooldown(spell);

            return 0;
        }

        public override string ToString()
        {
            return "Cooldown of " + spell.Name + " ends";
        }
    }
}
