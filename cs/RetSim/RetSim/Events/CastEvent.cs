using System.Collections.Generic;

namespace RetSim.Events
{
    public class CastEvent : Event
    {
        private const int CastEventPriority = 3;
        private readonly int spellId;

        //TODO change to Spell instead of id
        public CastEvent(int expirationTime, Player player, int spellId) : base(expirationTime, CastEventPriority, player)
        {
            this.spellId = spellId;
        }

        //TODO change to not return list but accept one
        public override int Execute(int time, List<Event> resultingEvents)
        {
            //MY CODE
            //player.Spellbook.ByID[spellId].Use(time, player); 

            //SULIS CODE
            //return player.CastSpell(spellId, time, resultingEvents);

            List<Event> result = player.Cast(player.Spellbook.ByID[spellId], ExpirationTime);
            resultingEvents.AddRange(result);

            return 0;
        }

        public override string ToString()
        {
            return "Cast " + Spells.ByID[spellId].Name;
        }
    }
}