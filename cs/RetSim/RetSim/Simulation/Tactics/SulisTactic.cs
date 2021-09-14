using RetSim.Simulation.Events;

namespace RetSim.Simulation.Tactics;

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