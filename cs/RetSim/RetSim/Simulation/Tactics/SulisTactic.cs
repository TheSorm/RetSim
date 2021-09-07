using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.Tactics
{
    public class SulisTactic : Tactic
    {
        public SulisTactic()
        {

        }

        public override List<Event> PreFight(FightSimulation fight)
        {
            return new List<Event>()
            {
                new AutoAttackEvent(fight, 0)
            };
        }

        public override Event GetActionBetween(int start, int end, FightSimulation fight)
        {
            return null;
        }
    }
}