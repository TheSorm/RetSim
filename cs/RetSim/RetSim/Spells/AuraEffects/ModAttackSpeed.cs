using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

class ModAttackSpeed : ModifyPercent
{
    public ModAttackSpeed(int percent) : base(percent)
    {
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        base.Apply(aura, caster, target, fight);

        fight.Player.Modifiers.AttackSpeed *= RelativeDifference;

        fight.Player.RecalculateAttack(fight);
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        fight.Player.Modifiers.AttackSpeed /= RelativeDifference;

        base.Remove(aura, caster, target, fight);

        fight.Player.RecalculateAttack(fight);
    }
}