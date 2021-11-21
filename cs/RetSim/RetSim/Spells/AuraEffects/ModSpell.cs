using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

class ModSpell : ModifyFlat
{
    public ModSpell(float amount, List<int> spells) : base(amount)
    {
        Spells = spells;
    }

    public List<int> Spells { get; init; }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (int spell in Spells)
        {
            fight.Player.Spellbook[spell].BonusCritChance += Value;
        }
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (int spell in Spells)
        {
            fight.Player.Spellbook[spell].BonusCritChance -= Value;
        }
    }
}