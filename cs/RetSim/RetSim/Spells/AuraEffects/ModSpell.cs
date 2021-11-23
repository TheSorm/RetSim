using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.Player.State;

namespace RetSim.Spells.AuraEffects;

class ModSpell : AuraEffect
{
    public List<int> Spells { get; init; }

    public int Cooldown { get; init; }
    public int ManaCost { get; init; }
    public float CritChance { get; init; }

    public float Effect1 { get; init; }
    public float Effect2 { get; init; }
    public float Effect3 { get; init; }

    public float AuraEffect1 { get; init; }
    public float AuraEffect2 { get; init; }
    public float AuraEffect3 { get; init; }

    public ModSpell(float amount, int cooldown, int manaCost, float critChance, float effect1, float effect2, float effect3, float auraEffect1, float auraEffect2, float auraEffect3, List<int> spells) : base(amount)
    {
        Spells = spells;

        Cooldown = cooldown;
        ManaCost = manaCost;
        CritChance = critChance;

        Effect1 = effect1;
        Effect2 = effect2;
        Effect3 = effect3;

        AuraEffect1 = auraEffect1;
        AuraEffect2 = auraEffect2;
        AuraEffect3 = auraEffect3;
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (int spell in Spells)
        {
            SpellState state = fight.Player.Spellbook[spell];

            if (Cooldown != 0)
                state.CooldownReduction += Cooldown;

            if (ManaCost != 0)
                state.ManaCostReduction += ManaCost;

            if (CritChance != 0)
                state.BonusCritChance += CritChance;

            if (Effect1 != 0f)
                state.EffectBonuses[0].Flat += Effect1;

            if (Effect2 != 0f)
                state.EffectBonuses[1].Flat += Effect2;

            if (Effect3 != 0f)
                state.EffectBonuses[2].Flat += Effect3;

            if (AuraEffect1 != 0f)
                state.AuraBonuses[0].Flat += AuraEffect1;

            if (AuraEffect2 != 0f)
                state.AuraBonuses[1].Flat += AuraEffect2;

            if (AuraEffect3 != 0f)
                state.AuraBonuses[2].Flat += AuraEffect3;
        }
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (int spell in Spells)
        {
            SpellState state = fight.Player.Spellbook[spell];

            if (Cooldown != 0)
                state.CooldownReduction -= Cooldown;

            if (ManaCost != 0)
                state.ManaCostReduction -= ManaCost;

            if (CritChance != 0)
                state.BonusCritChance -= CritChance;

            if (Effect1 != 0f)
                state.EffectBonuses[0].Flat -= Effect1;

            if (Effect2 != 0f)
                state.EffectBonuses[1].Flat -= Effect2;

            if (Effect3 != 0f)
                state.EffectBonuses[2].Flat -= Effect3;

            if (AuraEffect1 != 0f)
                state.AuraBonuses[0].Flat -= AuraEffect1;

            if (AuraEffect2 != 0f)
                state.AuraBonuses[1].Flat -= AuraEffect2;

            if (AuraEffect3 != 0f)
                state.AuraBonuses[2].Flat -= AuraEffect3;
        }
    }
}