using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

abstract class ModifyPercent : AuraEffect
{
    public ModifyPercent(int percent) : base()
    {
        Percent = percent;
    }

    public int Percent { get; init; }

    protected int CurrentMod = 100;
    protected float PreviousMod;
    protected float RelativeDifference;

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        CurrentMod = 100 + Percent * target.Auras[aura].Stacks;
        PreviousMod = CurrentMod - Percent;
        RelativeDifference = CurrentMod / PreviousMod;
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        CurrentMod = (int)PreviousMod;
        PreviousMod -= Percent;
        RelativeDifference = CurrentMod / PreviousMod;
    }
}