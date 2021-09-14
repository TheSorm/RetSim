using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

class ModAttackSpeed : ModifyPercent
{
    public bool Snapshots { get; init; }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        base.Apply(aura, caster, target, fight);

        float previousAttackSpeed = fight.Player.Weapon.EffectiveSpeed;

        fight.Player.Modifiers.AttackSpeed *= RelativeDifference;

        if (!Snapshots)
            fight.Player.RecalculateAttack(fight, previousAttackSpeed);
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        fight.Player.Modifiers.AttackSpeed /= RelativeDifference;

        base.Remove(aura, caster, target, fight);
    }
}