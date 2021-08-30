using System;
using System.Collections.Generic;

namespace RetSim
{
    public static class Formulas
    {
        public static class Damage
        {
            public static float GetDamageModifier(DamageResult result, Category crit, Player player)
            {
                return result switch
                {
                    DamageResult.Crit => 1 + GetCritDamage(crit, player),
                    DamageResult.Glancing => RollGlancingDamage(),
                    _ => 1f,
                };
            }

            public static float GetCritChance(Category category, Player player)
            {
                return category switch
                {
                    Category.Physical => player.Stats.CritChance,
                    Category.Spell => player.Stats.SpellCrit,
                    _ => 0f,
                };
            }

            public static float GetCritDamage(Category category, Player player)
            {
                return category switch
                {
                    Category.Physical => player.Stats.EffectiveCritDamage,
                    Category.Spell => player.Stats.EffectiveSpellCritDamage,
                    _ => 0f,
                };
            }

            public static float GetSchoolModifier(School school, Player player)
            {
                return school switch
                {
                    School.Physical => player.Modifiers.Physical,
                    School.Holy => player.Modifiers.Holy,
                    School.Shadow => player.Modifiers.Shadow,
                    School.Arcane => player.Modifiers.Arcane,
                    School.Fire => player.Modifiers.Fire,
                    School.Frost => player.Modifiers.Frost,
                    School.Nature => player.Modifiers.Nature,
                    _ => 1f,
                };
            }

            public static float RollGlancingDamage()
            {
                return RNG.RollRange(Constants.Boss.GlancePenaltyMin * 100, Constants.Boss.GlancePenaltyMax * 100) / 10000f;
            }


            public static bool CritCheck(float crit)
            {
                return RNG.RollRange(0, 10000) < Helpers.UpgradeFraction(crit);
            }
        }



        public static class HitChecks
        {
            public static readonly Dictionary<DefenseType, Func<float, float, float, Tuple<AttackResult, DamageResult>>> CheckToFunction = new()
            {
                { DefenseType.Auto, Auto },
                { DefenseType.Special, Special },
                { DefenseType.Ranged, Ranged },
                { DefenseType.Magic, Ranged },
                { DefenseType.None, None }
            };

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

                else if (Damage.CritCheck(crit))
                    damage = DamageResult.Crit;

                return Tuple.Create(attack, damage);
            }

            public static Tuple<AttackResult, DamageResult> Ranged(float miss, float dodge, float crit)
            {
                int random = RNG.RollRange(0, 10000);

                float m = Helpers.UpgradeFraction(miss);

                AttackResult attack = (random < m) ? AttackResult.Miss : AttackResult.Hit;
                DamageResult damage = attack == AttackResult.Hit && Damage.CritCheck(crit) ? DamageResult.Crit : DamageResult.None;

                return Tuple.Create(attack, damage);
            }

            public static Tuple<AttackResult, DamageResult> None(float miss, float dodge, float crit)
            {
                return Tuple.Create(AttackResult.Hit, Damage.CritCheck(crit) ? DamageResult.Crit : DamageResult.None);
            }

        }

    }
}
