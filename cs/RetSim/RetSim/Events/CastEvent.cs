using System.Collections.Generic;

namespace RetSim.Events
{
    public class CastEvent : Event
    {
        private const int CastEventPriority = 3;
        private readonly Spell spell;

        public CastEvent(int expirationTime, Player player, Spell spell) : base(expirationTime, CastEventPriority, player)
        {
            this.spell = spell;
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            player.Cast(spell, ExpirationTime, resultingEvents);
            return 0;
        }

        public override string ToString()
        {
            return "Cast " + spell.Name;
        }
    }
}