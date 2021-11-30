using RetSim.Misc;
using RetSim.Spells;
using RetSim.Spells.SpellEffects;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;

namespace RetSim.Simulation;

public class Attack
{
    private Player Player { get; init; }
    public Enemy Enemy { get; init; }
    private Damage Effect { get; init; }
    private SpellState Spell { get; init; }

    public AttackResult AttackResult { get; private set; }
    public DamageResult DamageResult { get; private set; }

    public float BaseDamage { get; private set; }
    public int Damage { get; private set; }
    public float Glancing { get; private set; }
    public float Mitigation { get; private set; }
    public float DamageResultMultiplier { get; private set; }

    public override string ToString()
    {
        return $"{Player} attacks {Enemy} with {Spell.Spell.Name} ({AttackResult} / {DamageResult}) - {Damage} Damage";
    }

    public Attack(Player player, Enemy enemy, Damage effect, SpellState state)
    {
        Player = player;
        Enemy = enemy;
        Effect = effect;
        Spell = state;

        ResolveAttack();
    }

    public void ResolveAttack()
    {
        float miss = GetMissChance(Player, Effect.DefenseCategory) - Enemy.Stats[StatName.IncreasedAttackerHitChance].Value;

        if (miss < 0)
            miss = 0;

        float dodge = Effect.IgnoresDefenses ? 0 : GetDodgeChance(Player, Effect.DefenseCategory);
        float crit = GetCritChance(Player, Effect.CritCategory) + Spell.BonusCritChance + Enemy.Stats[StatName.IncreasedAttackerCritChance].Value;

        var result = GetAttackResult(Effect.DefenseCategory, miss, dodge, crit);

        AttackResult = result.Attack;

        if (Effect.CritCategory == AttackCategory.None && result.Damage == DamageResult.Crit)
            DamageResult = DamageResult.None;

        else
            DamageResult = result.Damage;
    }

    public void ResolveDamage()
    {
        DamageResultMultiplier = GetDamageResultMultiplier(Player, DamageResult, Effect.CritCategory);

        Glancing = DamageResult == DamageResult.Glancing ? (1 - DamageResultMultiplier) * 100 : 0f;

        float mitigation = Effect.IgnoresDefenses ? 1f : GetMitigation(Player, Enemy, Effect.School);

        float playerDamage = Effect.CalculateDamage(Player, this, Spell);
        float bonusSpellPower = Effect.Coefficient == 0 ? 0 : Enemy.Modifiers.DamageTakenFlat[Effect.School] * Effect.Coefficient;

        BaseDamage = (playerDamage + bonusSpellPower) * Enemy.Modifiers.DamageTaken.GetModifier(Effect.School);

        Damage = RNG.RollDamage(BaseDamage * DamageResultMultiplier * mitigation);

        Mitigation = (1 - mitigation) * 100f;
    }

    public static (AttackResult Attack, DamageResult Damage) GetAttackResult(DefenseType defense, float miss, float dodge, float crit)
    {
        return defense switch
        {
            DefenseType.White => White(miss, dodge, crit),
            DefenseType.Special => Special(miss, dodge, crit),
            DefenseType.None => None(crit),
            _ => Ranged(miss, crit)
        };
    }

    public static bool CritCheck(float chance)
    {
        return RNG.RollRange(0, 10000) < Helpers.UpgradeFraction(chance);
    }

    public static (AttackResult, DamageResult) White(float miss, float dodge, float crit)
    {
        AttackResult attack = AttackResult.Hit;
        DamageResult damage = DamageResult.None;

        int random = RNG.RollRange(0, 10000);

        float m = Helpers.UpgradeFraction(miss);
        float d = Helpers.UpgradeFraction(dodge) + m;
        float g = Constants.Boss.UpgradedGlancingChance + d;
        float c = Helpers.UpgradeFraction(crit) + g;

        if (random < m)
            attack = AttackResult.Miss;

        else if (random < d)
            attack = AttackResult.Dodge;

        else if (random < g)
            damage = DamageResult.Glancing;

        else if (random < c)
            damage = DamageResult.Crit;

        return (attack, damage);
    }

    public static (AttackResult, DamageResult) Special(float miss, float dodge, float crit)
    {
        AttackResult attack = AttackResult.Hit;
        DamageResult damage = DamageResult.None;

        int random = RNG.RollRange(0, 10000);

        float m = Helpers.UpgradeFraction(miss);
        float d = Helpers.UpgradeFraction(dodge) + m;

        if (random < m)
            attack = AttackResult.Miss;

        else if (random < d)
            attack = AttackResult.Dodge;

        else if (CritCheck(crit))
            damage = DamageResult.Crit;

        return (attack, damage);
    }

    public static (AttackResult, DamageResult) Ranged(float miss, float crit)
    {
        int random = RNG.RollRange(0, 10000);

        float m = Helpers.UpgradeFraction(miss);

        AttackResult attack = random < m ? AttackResult.Miss : AttackResult.Hit;
        DamageResult damage = attack == AttackResult.Hit && CritCheck(crit) ? DamageResult.Crit : DamageResult.None;

        return (attack, damage);
    }

    public static (AttackResult, DamageResult) None(float crit)
    {
        return (AttackResult.Hit, CritCheck(crit) ? DamageResult.Crit : DamageResult.None);
    }

    public static float GetDamageResultMultiplier(Player player, DamageResult result, AttackCategory category)
    {
        return result switch
        {
            DamageResult.Crit => GetCritDamage(player, category),
            DamageResult.Glancing => 1 - RNG.RollGlancing(),
            _ => 1f,
        };
    }

    public static float GetCritDamage(Player player, AttackCategory category)
    {
        return category switch
        {
            AttackCategory.Physical => player.Stats[StatName.CritDamage].Value,
            AttackCategory.Spell => player.Stats[StatName.SpellCritDamage].Value,
            _ => 0f,
        };
    }

    public static float GetCritChance(Player player, AttackCategory category)
    {
        return category switch
        {
            AttackCategory.Physical => player.Stats.EffectiveCritChance,
            AttackCategory.Spell => player.Stats[StatName.SpellCrit].Value,
            _ => 0f
        };
    }

    public static float GetMissChance(Player player, DefenseType category)
    {
        return category switch
        {
            DefenseType.None => 0f,
            DefenseType.Magic => player.Stats.EffectiveSpellMissChance,
            _ => player.Stats.EffectiveMissChance
        };
    }

    public static float GetDodgeChance(Player player, DefenseType category)
    {
        return category switch
        {
            DefenseType.White or DefenseType.Special => player.Stats.EffectiveDodgeChance,
            _ => 0f,
        };
    }

    public static float GetMitigation(Player player, Enemy enemy, School school)
    {
        return school switch
        {
            School.Physical => GetArmorDR(player.Stats[StatName.ArmorPenetration].Value, enemy.Stats[StatName.Armor].Value),
            School.Typeless => 1f,
            _ => RNG.RollPartialResist()
        };
    }

    public static float GetArmorDR(float armorPenetration, float armor)
    {
        float effective = Math.Max(armor - armorPenetration, 0f);

        float reduction = effective / (effective + Constants.Boss.ArmorMagicNumber);

        if (reduction > 1f)
            return 0f;

        else
            return 1 - reduction;
    }
}

public enum AttackResult
{
    Miss = 0,
    Dodge = 1,
    Hit = 2
}

public enum DamageResult
{
    None = 0,
    Crit = 1,
    Glancing = 2
}