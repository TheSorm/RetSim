using RetSim.Simulation.Events;
using RetSim.Spells;

namespace RetSim.Units.Player.State;

public class SpellState
{
    public Spell Spell { get; init; }

    public CooldownEndEvent CooldownEnd { get; set; }
    public int CooldownReduction { get; set; }
    public int EffectiveCooldown => Spell.Cooldown - CooldownReduction;

    public int ManaCostReduction { get; set; }
    public float ManaCostReductionPercent { get; set; }
    public int EffectiveManaCost => (int)(Spell.ManaCost * (2 - ManaCostReductionPercent)) - ManaCostReduction;

    public EffectBonus[] EffectBonuses { get; set; }
    public EffectBonus[] AuraBonuses { get; set; }

    public int BonusSpellPower { get; set; }
    public float BonusCritChance { get; set; }

    public SpellState(Spell spell)
    {
        Spell = spell;

        CooldownEnd = null;
        CooldownReduction = 0;

        ManaCostReduction = 0;
        ManaCostReductionPercent = 1f;

        BonusSpellPower = 0;
        BonusCritChance = 0f;

        if (spell.Effects != null)
        {
            EffectBonuses = new EffectBonus[spell.Effects.Count];

            for (int i = 0; i < EffectBonuses.Length; i++)
            {
                EffectBonuses[i] = new EffectBonus();
            }
        }

        if (spell.Aura != null)
        { 
            AuraBonuses = new EffectBonus[spell.Aura.Effects.Count];

            for (int i = 0; i < AuraBonuses.Length; i++)
            {
                AuraBonuses[i] = new EffectBonus();
            }
        }
    }

    public override string ToString()
    {
        return Spell.Name;
    }
}

public class EffectBonus
{
    public int Flat { get; set; }
    public float Percent { get; set; }

    public EffectBonus()
    {
        Flat = 0;
        Percent = 1;
    }
}