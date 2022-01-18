using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.UnitStats;

namespace RetSim.Spells.AuraEffects;

class ModAttackSpeed : ModifyPercent
{
    public int HasteRating { get; init; }

    public ModAttackSpeed(float percent, int haste) : base(percent)
    {
        HasteRating = haste;
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        fight.Player.Modifiers.AttackSpeed *= GetDifference(Value, target.Auras[aura].Stacks);

        fight.Player.Stats[StatName.HasteRating].Bonus += HasteRating;

        fight.Player.RecalculateAttack(fight);

        fight.Player.HasteEndEvents.Add(fight.Player.Auras[aura].End);

        fight.Player.RecalculateNext();
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        fight.Player.Modifiers.AttackSpeed /= GetDifference(Value, target.Auras[aura].Stacks);

        fight.Player.Stats[StatName.HasteRating].Bonus -= HasteRating;

        fight.Player.RecalculateAttack(fight);

        fight.Player.HasteEndEvents.Remove(fight.Player.Auras[aura].End);
    }
}