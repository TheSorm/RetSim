using System.Collections.Generic;

namespace RetSim.Events
{
    public class AuraEndEvent : Event
    {
        private readonly int auraId;
        public AuraEndEvent(int expirationTime, Player player, int auraId) : base(expirationTime, player)
        {
            this.auraId = auraId;
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            player.RemoveAura(auraId);
            return 0;
        }

        public override string ToString()
        {
            return Auras.ByID[auraId].Name + " fades";
        }
    }
}
