using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.Player.State;

namespace RetSim.Spells.AuraEffects;

class ModSpellPercent : ModifyPercent
{
    public List<int> Spells { get; init; }

    public float ManaCost { get; init; }

    public float Effect1 { get; init; }
    public float Effect2 { get; init; }
    public float Effect3 { get; init; }

    public float AuraEffect1 { get; init; }
    public float AuraEffect2 { get; init; }
    public float AuraEffect3 { get; init; }

    public ModSpellPercent(float amount, float manaCost, float effect1, float effect2, float effect3, float auraEffect1, float auraEffect2, float auraEffect3, List<int> spells) : base(amount)
    {
        Spells = spells;

        ManaCost = manaCost;

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

            int stacks = fight.Player.Auras[aura].Stacks;

            if (ManaCost != 0)
                state.ManaCostReductionPercent *= GetDifference(ManaCost, stacks);

            if (Effect1 != 0f)
                state.EffectBonuses[0].Percent *= GetDifference(Effect1, stacks);

            if (Effect2 != 0f)
                state.EffectBonuses[1].Percent *= GetDifference(Effect2, stacks);

            if (Effect3 != 0f)
                state.EffectBonuses[2].Percent *= GetDifference(Effect3, stacks);

            if (AuraEffect1 != 0f)
                state.AuraBonuses[0].Percent *= GetDifference(AuraEffect1, stacks);

            if (AuraEffect2 != 0f)
                state.AuraBonuses[1].Percent *= GetDifference(AuraEffect2, stacks);

            if (AuraEffect3 != 0f)
                state.AuraBonuses[2].Percent *= GetDifference(AuraEffect3, stacks);
        }
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (int spell in Spells)
        {
            SpellState state = fight.Player.Spellbook[spell];

            int stacks = fight.Player.Auras[aura].Stacks;

            if (ManaCost != 0)
                state.ManaCostReductionPercent /= GetDifference(ManaCost, stacks);

            if (Effect1 != 0f)
                state.EffectBonuses[0].Percent /= GetDifference(Effect1, stacks);

            if (Effect2 != 0f)
                state.EffectBonuses[1].Percent /= GetDifference(Effect2, stacks);

            if (Effect3 != 0f)
                state.EffectBonuses[2].Percent /= GetDifference(Effect3, stacks);

            if (AuraEffect1 != 0f)
                state.AuraBonuses[0].Percent /= GetDifference(AuraEffect1, stacks);

            if (AuraEffect2 != 0f)
                state.AuraBonuses[1].Percent /= GetDifference(AuraEffect2, stacks);

            if (AuraEffect3 != 0f)
                state.AuraBonuses[2].Percent /= GetDifference(AuraEffect3, stacks);
        }
    }
}