using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim.Data
{
    public static partial class Auras
    {
        public static readonly Aura ImprovedSealOfTheCrusader = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { CritChance = 3, SpellCrit = 3 } }
            }
        };

        public static readonly Aura Conviction = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { CritChance = 5 } }
            }
        };

        public static readonly Aura Crusade = new()
        {
            Effects = new List<AuraEffect>()
            {
                new ModDamageCreature()
                {
                    Percentage = 5,
                    Types = new List<CreatureType> { CreatureType.Humanoid, CreatureType.Demon, CreatureType.Undead, CreatureType.Elemental },
                    Schools = new List<School> { School.Typeless, School.Physical, School.Holy, School.Fire, School.Nature, School.Frost, School.Shadow, School.Arcane }
                }
            }
        };

        public static readonly Aura TwoHandedWeaponSpecialization = new()
        {
        };

        public static readonly Aura SanctityAura = new()
        {
            Effects = new List<AuraEffect>() { new ModDamageSchool() { Percentage = 10, Schools = new List<School> { School.Holy } } }
        };

        public static readonly Aura ImprovedSanctityAura = new()
        {
            Effects = new List<AuraEffect>()
            {
                new ModDamageSchool()
                {
                    Percentage = 2,
                    Schools = new List<School> { School.Typeless, School.Physical, School.Holy, School.Fire, School.Nature, School.Frost, School.Shadow, School.Arcane }
                }
            }
        };

        public static readonly Aura SanctifiedSeals = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { CritChance = 3, SpellCrit = 3 } }
            }
        };

        public static readonly Aura Vengeance = new()
        {
        };

        public static readonly Aura VengeanceProc = new()
        {
            Duration = 30000,
            MaxStacks = 3,
            Effects = new List<AuraEffect>() { new ModDamageSchool() { Percentage = 5, Schools = new List<School> { School.Physical, School.Holy, } } }
        };

        public static readonly Aura Precision = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { HitChance = 3, SpellHit = 3 } }
            }
        };

        public static readonly Aura DivineStrength = new()
        {
            Effects = new List<AuraEffect>() { new ModStat() { Percentage = 10, Strength = true } }
        };
    }
}