using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

class GainSeal : GainProc
{
    public GainSeal(int procID) : base(procID)
    {
    }

    private Seal Seal { get; set; }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        Seal = (Seal)aura;

        foreach (Seal other in Seal.ExclusiveWith)
        {
            if (target.Auras.IsActive(other))
            {
                if (other.Persist == 0)
                    target.Auras[other].End.Timestamp = fight.Timestamp;

                else
                    target.Auras[other].End.Timestamp = fight.Timestamp + other.Persist;
            }
        }

        target.Auras.CurrentSeal = Seal;

        base.Apply(aura, caster, target, fight);
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        if (target.Auras.CurrentSeal != null && target.Auras.CurrentSeal == Seal)
            target.Auras.CurrentSeal = null;

        base.Remove(aura, caster, target, fight);
    }
}