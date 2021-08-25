using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.Tactics
{
    public class SulisTactic : Tactic
    {
        public SulisTactic()
        {

        }

        public override List<Event> PreFight(Player player)
        {
            return new List<Event>()
            {
                new AutoAttackEvent(0, player)
            };
        }

        public override Event GetActionBetween(int start, int end, Player player)
        {
            return null;
        }
    }
}