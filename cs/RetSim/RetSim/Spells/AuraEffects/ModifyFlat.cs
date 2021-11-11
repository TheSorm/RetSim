using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

abstract class ModifyFlat : AuraEffect
{
    public int Amount { get; init; }

    protected int CurrentAmount;
    protected int PreviousAmount;
    protected int Difference;

    public ModifyFlat(int amount)
    {
        Amount = amount;
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        CurrentAmount = Amount * fight.Player.Auras[aura].Stacks;
        PreviousAmount = CurrentAmount - Amount;
        Difference = CurrentAmount - PreviousAmount;
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        CurrentAmount = PreviousAmount;
        PreviousAmount -= Amount;
        Difference = CurrentAmount - PreviousAmount;
    }
}