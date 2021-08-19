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
            //MY CODE
            //player.Spellbook.ByID[spellId].Use(time, player); 

            //SULIS CODE
            //return player.CastSpell(spellId, time, resultingEvents);

            return 0;
        }

        public override string ToString()
        {
            return "Cast " + Spells.ByID[spellId].Name;
        }
    }
}