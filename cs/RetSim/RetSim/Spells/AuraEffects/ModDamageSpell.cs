using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

class ModDamageSpell : ModifyPercent
{
    public List<int> Spells { get; init; }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        base.Apply(aura, caster, target, fight);

        foreach (int spell in Spells)
        {
            fight.Player.Spellbook[spell].EffectBonusPercent *= RelativeDifference;
        }
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (int spell in Spells)
        {
            fight.Player.Spellbook[spell].EffectBonusPercent /= RelativeDifference;
        }

        base.Remove(aura, caster, target, fight);
    }
}