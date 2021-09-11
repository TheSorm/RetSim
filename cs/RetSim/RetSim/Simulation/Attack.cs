using RetSim.SpellEffects;

namespace RetSim
{
    public class Attack
    {
        private Player Player { get; init; }
        private Enemy Enemy { get; init; }
        private DamageEffect Effect { get; init; }
        private SpellState State { get; init; }

        public AttackResult AttackResult { get; private set; }
        public DamageResult DamageResult { get; private set; }

        public float BaseDamage { get; private set; }
        public int Damage { get; private set; }

        public float Glancing { get; private set; }
        public float Mitigation { get; private set; }

        public float SpellPowerBonus => Effect.Coefficient * (Player.Stats[StatName.SpellPower].Value + State.BonusSpellPower);
        public float SchoolModifier => Player.Modifiers.Schools.GetValue(Effect.School);
        public float DamageModifier { get; private set; }

        public float JotCBonus => Effect.HolyCoefficient == 0 ? SpellPowerBonus : Effect.HolyCoefficient * (Player.Stats[StatName.SpellPower].Value + State.BonusSpellPower);

        public Attack(Player player, Enemy enemy, DamageEffect effect, SpellState state)
        {
            Player = player;
            Enemy = enemy;
            Effect = effect;
            State = state;

            ResolveAttack();
        }

        public void ResolveAttack()
        {
            float miss = GetMissChance(Player, Effect.DefenseCategory);
            float dodge = GetDodgeChance(Player, Effect.DefenseCategory);
            float crit = GetCritChance(Player, Effect.CritCategory) + State.BonusCritChance;

            var result = GetAttackResult(Effect.DefenseCategory, miss, dodge, crit);

            AttackResult = result.Attack;
            DamageResult = result.Damage;

            DamageModifier = GetDamageModifier(Player, DamageResult, Effect.CritCategory);

            if (DamageResult == DamageResult.Glancing)
                Glancing = DamageResult == DamageResult.Glancing ? (1 - DamageModifier) * 100 : 0f;

            Mitigation = AttackResult == AttackResult.Hit ? GetMitigation(Player, Enemy, Effect.School) : 0;
        }

        public void ResolveDamage()
        {
            DamageModifier = GetDamageModifier(Player, DamageResult, Effect.CritCategory);

            if (DamageResult == DamageResult.Glancing)
                Glancing = DamageResult == DamageResult.Glancing ? (1 - DamageModifier) * 100 : 0f;

            float mitigation = AttackResult == AttackResult.Hit ? GetMitigation(Player, Enemy, Effect.School) : 0; //TODO: Fix partial resist crits?

            BaseDamage = Effect.CalculateDamage(Player, this, State);

            Damage = RNG.RollDamage(BaseDamage * mitigation);

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

            AttackResult attack = (random < m) ? AttackResult.Miss : AttackResult.Hit;
            DamageResult damage = attack == AttackResult.Hit && CritCheck(crit) ? DamageResult.Crit : DamageResult.None;

            return (attack, damage);
        }

        public static (AttackResult, DamageResult) None(float crit)
        {
            return (AttackResult.Hit, CritCheck(crit) ? DamageResult.Crit : DamageResult.None);
        }

        public static float GetDamageModifier(Player player, DamageResult result, AttackCategory category)
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
                AttackCategory.Physical => player.Stats[StatName.CritChance].Value,
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
                School.Physical => GetArmorDR(player, enemy),
                School.Typeless => 1f,
                _ => RNG.RollPartialResist()
            };
        }

        public static float GetArmorDR(Player player, Enemy enemy)
        {
            int effective = enemy.EffectiveArmor((int)player.Stats[StatName.ArmorPenetration].Value);

            float dr = effective / (effective + Constants.Boss.ArmorMagicNumber);

            return dr > 1f ? 0f : 1 - dr;
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
}