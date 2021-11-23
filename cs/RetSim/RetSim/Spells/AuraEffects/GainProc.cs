using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

public class GainProc : AuraEffect
{
    public Proc Proc { get; init; }

    public GainProc(float proc) : base(proc)
    {
        if (proc != 0)
            Proc = Data.Collections.Procs[(int)proc];
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        if (Proc != null)
            fight.Player.Procs.Add(Proc);
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        if (Proc != null)
            fight.Player.Procs.Remove(Proc);
    }
}