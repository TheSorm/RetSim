using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim.Data
{
    public static partial class Auras
    {
        public static readonly Aura GreaterBlessingOfMight = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.AttackPower, 220 } } }
            }
        };

        public static readonly Aura GreaterBlessingOfKings = new()
        {
            Effects = new List<AuraEffect>
            {
                new ModStat() { Percentage = 10, All = true }
            }
        };

        public static readonly Aura GreaterBlessingOfWisdom = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.ManaPer5, 41 } } }
            }
        };

        public static readonly Aura BattleShout = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() {{ StatName.AttackPower, 306 } } }
            }
        };

        public static readonly Aura TrueshotAura = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.AttackPower, 125 } } }
            }
        };

        public static readonly Aura FerociousInspiration = new()
        {
            Effects = new List<AuraEffect>()
            {
                new ModDamageSchool()
                {
                    Percentage = 3,
                    Schools = Spells.AllSchools
                }
            }
        };

        public static readonly Aura StrengthOfEarthTotem = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.Strength, 86 } } }
            }
        };

        public static readonly Aura GraceOfAirTotem = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.Agility, 77 } } }
            }
        };

        public static readonly Aura ManaSpringTotem = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.ManaPer5, 50 } } }
            }
        };

        public static readonly Aura UnleashedRage = new()
        {
            Effects = new List<AuraEffect>
            {
                new ModStat() { Percentage = 10, AttackPower = true }
            }
        };

        public static readonly Aura MarkOfTheWild = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.Armor, 340 }, { StatName.Strength, 14 }, { StatName.Agility, 14 }, { StatName.Intellect, 14 }, { StatName.Stamina, 14 } } }
            }
        };

        public static readonly Aura LeaderOfThePack = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.CritChance, 5 } } }
            }
        };

        public static readonly Aura PrayerOfFortitude = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.Stamina, 79 } } }
            }
        };

        public static readonly Aura PrayerOfSpirit = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.SpellPower, 20 } } }
            }
        };

        public static readonly Aura ArcaneBrilliance = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.Intellect, 40 } } }
            }
        };

        public static readonly Aura HeroicPresence = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.HitChance, 1 } } }
            }
        };

        public static readonly Aura InspiringPresence = new()
        {
            Effects = new()
            {
                new GainStats() { Stats = new() { { StatName.SpellHit, 1 } } }
            }
        };
    }
}