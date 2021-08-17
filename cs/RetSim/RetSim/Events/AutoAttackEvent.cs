using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetSim.Events
{
    public class AutoAttackEvent : Event
    {
        public AutoAttackEvent(int expirationTime, Player player) : base(expirationTime, player)
        {
        }

        public override int Execute(List<Event> resultingEvents, int time)
        {
            return player.MeleeAttack(time, resultingEvents); 
        }

        public override string ToString()
        {
            return "Auto attack lands";
        }
    }
}
