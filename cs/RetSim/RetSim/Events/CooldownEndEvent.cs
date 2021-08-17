
using System.Collections.Generic;

namespace RetSim
{
    internal class CooldownEndEvent : Event
    {
        private int spellId;

        public CooldownEndEvent(int expirationTime, Player player, int spellId) : base(expirationTime, player)
        {
            this.spellId = spellId;
        }

        internal override int Execute(List<Event> resultingEvents, int time)
        {
            return 0;
        }

        public override string ToString()
        {
            return "Cooldown of " + Spellbook.byID[spellId].Name + " ends";
        }
    }
}
