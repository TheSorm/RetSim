
using System.Collections.Generic;

namespace RetSim.Events
{
    public class CooldownEndEvent : Event
    {
        private readonly int spellId;

        public CooldownEndEvent(int expirationTime, Player player, int spellId) : base(expirationTime, player)
        {
            this.spellId = spellId;
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            player.RemoveCooldownOf(spellId);
            return 0;
        }

        public override string ToString()
        {
            return "Cooldown of " + Spellbook.ByID[spellId].Name + " ends";
        }
    }
}
