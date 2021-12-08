using RetSim.Spells;
using RetSim.Units;

namespace RetSim.Simulation.Events;

public class AuraEndEvent : Event
{
    private const int BasePriority = 1;

    public Aura Aura { get; init; }
    private Unit Caster { get; init; }
    private Unit Target { get; init; }

    public AuraEndEvent(Aura aura, Unit caster, Unit target, FightSimulation fight, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
    {
        Aura = aura;
        Caster = caster;
        Target = target;
    }

    public override ProcMask Execute(object arguments = null)
    {
        Target.Auras.Cancel(Aura, Caster, Target, Fight);

        return ProcMask.None;
    }

    public override string ToString()
    {
        return $"{Aura.Parent.Name} fades";
    }
}