using RetSim.Spells;

namespace RetSim.Simulation.Events;

public class GCDEndEvent : Event
{
    private const int BasePriority = 0;

    public GCDEndEvent(FightSimulation simulation, int timestamp, int priority = 0) : base(simulation, timestamp, priority + BasePriority)
    {
        Fight.Player.GCD.Start(this);
    }

    public override ProcMask Execute(object arguments = null)
    {
        Fight.Player.GCD.End();

        return ProcMask.None;
    }

    public override string ToString()
    {
        return "GCD ends";
    }
}