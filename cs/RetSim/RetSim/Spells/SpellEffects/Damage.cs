using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSim.Units.Player;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;

namespace RetSim.Spells.SpellEffects;

public class Damage : SpellEffect
{
    public School School { get; init; }
    public float Coefficient { get; init; }

    public DefenseType DefenseCategory { get; init; }
    public AttackCategory CritCategory { get; init; }
    public bool IgnoresDefenses { get; init; }

    public ProcMask OnCast { get; init; }
    public ProcMask OnHit { get; init; }
    public ProcMask OnCrit { get; init; }

    public Damage(float value, float dieSides, School school, float coefficient, DefenseType defense, AttackCategory crit, bool ignoresDefense, ProcMask onCast, ProcMask onHit, ProcMask onCrit) : base(value, dieSides)
    {
        School = school;
        Coefficient = coefficient;
        DefenseCategory = defense;
        CritCategory = crit;
        IgnoresDefenses = ignoresDefense;
        OnCast = onCast;
        OnHit = onHit;
        OnCrit = onCrit;
    }

    public virtual float GetBaseDamage(Player player, SpellState state)
    {
        return RNG.RollRange(Value, Value + DieSides);
    }

    public virtual float GetSpellPowerBonus(Player player, SpellState state)
    {
        if (Coefficient == 0)
            return 0;

        else
            return Coefficient * (player.Stats[StatName.SpellPower].Value + state.BonusSpellPower);
    }

    public virtual float CalculateDamage(Player player, Attack attack, SpellState state)
    {
        School school = School;

        if (CritCategory == AttackCategory.Physical)
            school |= School.Physical;

        float schoolMultiplier = player.Modifiers.DamageDone.GetModifier(school);

        EffectBonus bonus = state.EffectBonuses[state.Spell.Effects.IndexOf(this)];

        return ((GetBaseDamage(player, state) + GetSpellPowerBonus(player, state)) * bonus.Percent + bonus.Flat) * schoolMultiplier;
    }

    public override ProcMask Resolve(FightSimulation fight, SpellState state)
    {
        ProcMask mask = OnCast;

        var attack = new Attack(fight.Player, fight.Enemy, this, state);

        if (attack.AttackResult == AttackResult.Hit)
        {
            mask |= OnHit;

            if (attack.DamageResult == DamageResult.Crit)
                mask |= OnCrit;

            attack.ResolveDamage();
        }

        var entry = new DamageEntry()
        {
            Timestamp = fight.Timestamp,
            Mana = (int)fight.Player.Stats[StatName.Mana].Value,
            Source = Parent.Name,
            AttackResult = attack.AttackResult,
            Damage = attack.Damage,
            School = School,
            Crit = attack.DamageResult == DamageResult.Crit,
            Glancing = attack.Glancing,
            Mitigation = attack.Mitigation
        };

        fight.CombatLog.Add(entry);

        return mask;
    }
}