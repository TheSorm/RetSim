using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetSim.Events
{
    class AuraEndEvent : Event
    {
        private int auraId;
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
