using System.Collections.Generic;

namespace RetSim.Events
{
    public class CastEvent : Event
    {
        private const int CastEventPriority = 3;
        private readonly int spellId;
        public CastEvent(int expirationTime, Player player, int spellId) : base(expirationTime, CastEventPriority, player)
        {
            this.spellId = spellId;
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            return player.CastSpell(spellId, time, resultingEvents);
        }

        public override string ToString()
        {
            return "Cast " + Spellbook.ByID[spellId].Name;
        }
    }
}