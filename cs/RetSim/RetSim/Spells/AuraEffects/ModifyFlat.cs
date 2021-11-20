using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

abstract class ModifyFlat : AuraEffect
{
    protected float CurrentAmount;
    protected float PreviousAmount;
    protected float Difference;

    public ModifyFlat(float amount) : base(amount) { }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        CurrentAmount = Value * fight.Player.Auras[aura].Stacks;
        PreviousAmount = CurrentAmount - Value;
        Difference = CurrentAmount - PreviousAmount;
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        CurrentAmount = Value * (target.Auras[aura].Stacks - 1);
        PreviousAmount = CurrentAmount - Value;
        Difference = CurrentAmount - PreviousAmount;
    }
}