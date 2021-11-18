using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

abstract class ModifyPercent : AuraEffect
{
    protected float Current = 100;
    protected float Previous;
    protected float Difference;

    public ModifyPercent(float percent) : base(percent) { }


    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        Current = 100 + Value * target.Auras[aura].Stacks;
        Previous = Current - Value;
        Difference = Current / Previous;
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        Current = (int)Previous;
        Previous -= Value;
        Difference = Current / Previous;
    }
}