using RetSim.SpellEffects;
using System;
using System.Collections.Generic;

namespace RetSim
{    
    public class Attack
    {
        private Player Player { get; init; }
        private Enemy Enemy {  get; init; }
        private DamageEffect Effect { get; init; }

        public AttackResult AttackResult { get; private set; }
        public DamageResult DamageResult { get; private set; }

        public float BaseDamage { get; private set; }    
        public int Damage { get; private set; }

        public float Glancing { get; private set; }
        public float Mitigation { get; private set; }

        public float SpellPowerBonus => Effect.Coefficient == 0 ? 0 : Effect.Coefficient * (Player.Stats.SpellPower + Player.Modifiers.Bonuses[Effect.Spell]);
        public float SchoolModifier => Player.Modifiers.Schools.GetValue(Effect.School);
        public float SpellModifier => Player.Modifiers.Spells.GetValue(Effect.Spell);
        public float DamageModifier { get; private set; }

        public float JotCBonus => Effect.HolyCoefficient == 0 ? SpellPowerBonus : Effect.HolyCoefficient * (Player.Stats.SpellPower + Player.Modifiers.Bonuses[Effect.Spell]);

        public Attack(Player player, Enemy enemy, DamageEffect effect)
        {
            Player = player;
            Enemy = enemy;
            Effect = effect;

            ResolveAttack();
        }

        public void ResolveAttack()
        {
            float miss = GetMissChance(Player, Effect.DefenseCategory);
            float dodge = GetDodgeChance(Player, Effect.DefenseCategory);
            float crit = GetCritChance(Player, Effect.CritCategory);

            var result = AttackFormulas[Effect.DefenseCategory].Invoke(miss, dodge, crit);

            AttackResult = result.Item1;
            DamageResult = result.Item2;

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

            BaseDamage = Effect.CalculateDamage(Player, this); 

            Damage = RNG.RollDamage(BaseDamage * mitigation);

            Mitigation = (1 - mitigation) * 100f; 
        }

        public static readonly Dictionary<DefenseType, Func<float, float, float, Tuple<AttackResult, DamageResult>>> AttackFormulas = new()
        {
            { DefenseType.Auto, Auto },
            { DefenseType.Special, Special },
            { DefenseType.Ranged, Ranged },
            { DefenseType.Magic, Ranged },
            { DefenseType.None, None }
        };

        public static bool CritCheck(float chance)
        {
            return RNG.RollRange(0, 10000) < Helpers.UpgradeFraction(chance);
        }        

        public static Tuple<AttackResult, DamageResult> Auto(float miss, float dodge, float crit)
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

            return Tuple.Create(attack, damage);
        }

        public static Tuple<AttackResult, DamageResult> Special(float miss, float dodge, float crit)
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

            return Tuple.Create(attack, damage);
        }

        public static Tuple<AttackResult, DamageResult> Ranged(float miss, float dodge, float crit)
        {
            int random = RNG.RollRange(0, 10000);

            float m = Helpers.UpgradeFraction(miss);

            AttackResult attack = (random < m) ? AttackResult.Miss : AttackResult.Hit;
            DamageResult damage = attack == AttackResult.Hit && CritCheck(crit) ? DamageResult.Crit : DamageResult.None;

            return Tuple.Create(attack, damage);
        }

        public static Tuple<AttackResult, DamageResult> None(float miss, float dodge, float crit)
        {
            return Tuple.Create(AttackResult.Hit, CritCheck(crit) ? DamageResult.Crit : DamageResult.None);
        }

        public static float GetDamageModifier(Player player, DamageResult result, Category category)
        {
            return result switch
            {
                DamageResult.Crit => 1 + GetCritDamage(player, category),
                DamageResult.Glancing => 1 - RNG.RollGlancing(),
                _ => 1f,
            };
        }

        public static float GetCritDamage(Player player, Category category)
        {
            return category switch
            {
                Category.Physical => player.Stats.EffectiveCritDamage,
                Category.Spell => player.Stats.EffectiveSpellCritDamage,
                _ => 0f,
            };
        }

        public static float GetCritChance(Player player, Category category)
        {
            return category switch
            {
                Category.Physical => player.Stats.CritChance,
                Category.Spell => player.Stats.SpellCrit,
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
                DefenseType.Auto or DefenseType.Special => player.Stats.EffectiveDodgeChance,
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
            int effective = enemy.EffectiveArmor(player.Stats.ArmorPenetration);

            float dr = effective / (effective + Constants.Boss.ArmorMagicNumber);

            return dr > 1f ? 0f : 1 - dr;
        }
    }
}