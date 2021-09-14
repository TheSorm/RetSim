using RetSim.Spells;

namespace RetSim.Simulation.Events;

public class ProcCooldownEndEvent : Event
{
    private const int BasePriority = 2;

    private Proc Proc { get; init; }

    public ProcCooldownEndEvent(Proc proc, FightSimulation fight, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
    {
        Proc = proc;

        Fight.Player.Procs.StartCooldown(proc, this);
    }

    public override ProcMask Execute(object arguments = null)
    {
        Fight.Player.Procs.EndCooldown(Proc);

        return ProcMask.None;
    }

    public override string ToString()
    {
        return $"Cooldown of {Proc.Spell.Name} ends";
    }
}