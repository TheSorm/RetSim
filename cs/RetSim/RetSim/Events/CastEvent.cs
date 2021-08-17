using System.Collections.Generic;

namespace RetSim
{
    public class CastEvent : Event
    {
        private int spellId;
        public CastEvent(int expirationTime, Player player, int spellId) : base(expirationTime, player)
        {
            this.spellId = spellId;
        }

        public override int Execute(List<Event> resultingEvents, int time)
        {
            return player.CastSpell(spellId, time, resultingEvents);
        }

        public override string ToString()
        {
            return "Cast " + Spellbook.ByID[spellId].Name;
        }
    }
}