using RetSim.Misc;
using RetSim.Spells;

namespace RetSim.Units.UnitStats;

public class Stats
{
    protected Unit Parent { get; init; }
    public Stat[] All { get; init; }
    public Stat this[StatName key]
    {
        get => All[(int)key];
        protected set => All[(int)key] = value;
    }

    public Stats(Unit parent)
    {
        Parent = parent;
    }

    public float HitPenalty => Math.Min(this[StatName.HitChance].Value, 1f);
    public float EffectiveHitChance => this[StatName.HitChance].Value - HitPenalty;
    public float EffectiveMissChance => Math.Max(Constants.Boss.MissChance - EffectiveHitChance, 0);

    public float EffectiveSpellMissChance
    {
        get
        {
            float miss = Constants.Boss.SpellMissChance - this[StatName.SpellHit].Value;

            return miss > Constants.Boss.MininumSpellMissChance ? Constants.Boss.MininumSpellMissChance : 0;
        }
    }

    public float DodgeChanceReduction => Math.Min(this[StatName.Expertise].Value, Constants.Stats.ExpertiseCap) / Constants.Stats.ExpertisePerDodge;
    public float EffectiveDodgeChance => Math.Max(Constants.Boss.DodgeChance - DodgeChanceReduction, 0);

    public float EffectiveAttackSpeed => (1 + this[StatName.Haste].Value * 0.01f) * Parent.Modifiers.AttackSpeed;

    public float EffectiveCastSpeed => (1 + this[StatName.SpellHaste].Value * 0.01f) * Parent.Modifiers.CastSpeed;

    public int EffectiveGCD(Spell spell) => Math.Max((int)(spell.GCD.Duration / EffectiveCastSpeed), Constants.Numbers.MinimumGCD);
}