using RetSim.Simulation.Events;

namespace RetSim.Simulation.Tactics;

abstract public class Tactic
{
    public abstract List<Event> PreFight(FightSimulation fight);

    public abstract Event GetActionBetween(int start, int end, FightSimulation fight);
}