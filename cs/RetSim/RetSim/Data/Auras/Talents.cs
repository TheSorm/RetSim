using RetSim.AuraEffects;

namespace RetSim.Data
{
    public static partial class Auras
    {
        public static readonly Aura ImprovedSealOfTheCrusader = new()
        {
            Effects = new()
            {
                new ModifyStats() { Stats = new() { { StatName.CritChance, 3 }, { StatName.SpellCrit, 3 } } }
            }
        };

        public static readonly Aura Conviction = new()
        {
            Effects = new()
            {
                new ModifyStats() { Stats = new() { { StatName.CritChance, 5 } } }
            }
        };

        public static readonly Aura Crusade = new()
        {
            Effects = new()
            {
                new ModDamageCreature()
                {
                    Percentage = 3,
                    Types = new() { CreatureType.Humanoid, CreatureType.Demon, CreatureType.Undead, CreatureType.Elemental },
                    Schools = Spells.AllSchools
                }
            }
        };

        public static readonly Aura TwoHandedWeaponSpecialization = new()
        {
        };

        public static readonly Aura SanctityAura = new()
        {
            Effects = new() { new ModDamageSchool() { Percentage = 10, Schools = new() { School.Holy } } }
        };

        public static readonly Aura ImprovedSanctityAura = new()
        {
            Effects = new() { new ModDamageSchool() { Percentage = 2, Schools = Spells.AllSchools } }
        };

        public static readonly Aura SanctifiedSeals = new()
        {
            Effects = new()
            {
                new ModifyStats() { Stats = new() { { StatName.CritChance, 3 }, { StatName.SpellCrit, 3 } } }
            }
        };

        public static readonly Aura Vengeance = new()
        {
        };

        public static readonly Aura VengeanceProc = new()
        {
            Duration = 30000,
            MaxStacks = 3,
            Effects = new() { new ModDamageSchool() { Percentage = 5, Schools = new() { School.Physical, School.Holy } } }
        };

        public static readonly Aura Fanaticism = new()
        {
            Effects = new()
            {
                new ModSpellCritChance() { Amount = 15 } 
            }
        };

        public static readonly Aura Precision = new()
        {
            Effects = new()
            {
                new ModifyStats() { Stats = new() { { StatName.HitChance, 3 }, { StatName.SpellHit, 3 } } }
            }
        };

        public static readonly Aura DivineStrength = new()
        {
            Effects = new() { new ModStat() { Percentage = 10, Stats = new() { StatName.Strength } } }
        };

        public static readonly Aura DivineIntellect = new()
        {
            Effects = new() { new ModStat() { Percentage = 10, Stats = new() { StatName.Intellect } } }
        };
    }
}