using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim.Data
{
    public static partial class Auras
    {
        public static readonly Aura GreaterBlessingOfMight = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { AttackPower = 220 } }
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
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { ManaPer5 = 41 } }
            }
        };

        public static readonly Aura BattleShout = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { AttackPower = 306 } }
            }
        };

        public static readonly Aura TrueshotAura = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { AttackPower = 125 } }
            }
        };

        public static readonly Aura FerociousInspiration = new()
        {
            Effects = new List<AuraEffect>()
            {
                new ModDamageSchool()
                {
                    Percentage = 3,
                    Schools = new List<School> { School.Typeless, School.Physical, School.Holy, School.Fire, School.Nature, School.Frost, School.Shadow, School.Arcane }
                }
            }
        };

        public static readonly Aura StrengthOfEarthTotem = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { Strength = 86 } }
            }
        };

        public static readonly Aura GraceOfAirTotem = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { Agility = 77 } }
            }
        };

        public static readonly Aura ManaSpringTotem = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { ManaPer5 = 50 } }
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
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { Armor = 340, Strength = 14, Agility = 14, Intellect = 14, Stamina = 14 } }
            }
        };

        public static readonly Aura LeaderOfThePack = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { CritChance = 5 } }
            }
        };

        public static readonly Aura PrayerOfFortitude = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { Stamina = 79 } }
            }
        };

        public static readonly Aura PrayerOfSpirit = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { SpellPower = 20 } }
                //Spirit = 79
                //TODO: wat do about this
            }
        };

        public static readonly Aura ArcaneBrilliance = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { Intellect = 40 } }
            }
        };

        public static readonly Aura HeroicPresence = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { HitChance = 1 } }
            }
        };

        public static readonly Aura InspiringPresence = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { SpellHit = 1 } }
            }
        };
    }
}