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

    public int EffectBonus { get; set; }
    public float EffectBonusPercent { get; set; }

    public int BonusSpellPower { get; set; }
    public float BonusCritChance { get; set; }

    public SpellState(Spell spell)
    {
        Spell = spell;

        CooldownEnd = null;
        CooldownReduction = 0;

        ManaCostReduction = 0;
        ManaCostReductionPercent = 1f;

        EffectBonus = 0;
        EffectBonusPercent = 1f;

        BonusSpellPower = 0;
        BonusCritChance = 0f;
    }
}