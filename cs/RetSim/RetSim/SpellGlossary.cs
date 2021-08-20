using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public record Spell
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int ManaCost { get; init; }
        public int Cooldown { get; init; }
        public int CastTime { get; init; }
        public School School { get; init; }
        public GCD GCD { get; init; }
        public List<SpellEffect> Effects { get; init; }
    }

    public record GCD
    {
        public int Duration { get; init; }
        public GCDCategory Category { get; init; }
    }

    public enum GCDCategory
    {
        None = 0,
        Physical = 1,
        Spell = 2
    }

    public enum School
    {
        None = 0,
        Physical = 1,
        Holy = 2,
        Fire = 3,
        Nature = 4,
        Frost = 5,
        Shadow = 6,
        Arcane = 7
    }

    public static class SpellGlossary
    {
        public static readonly Spell CrusaderStrike = new()
        {
            ID = 35395,
            Name = "Crusader Strike",
            ManaCost = 236,
            Cooldown = 6000,
            CastTime = 0,
            School = School.Physical,
            GCD = new GCD() { Duration = 1500, Category = GCDCategory.Physical },
            Effects = new List<SpellEffect>()
            { new NormalizedWeaponDamage(1.1f) }
        };

        public static readonly Spell SealOfCommand = new()
        {
            ID = 27170,
            Name = "Seal of Command",
            ManaCost = 280,
            Cooldown = 0,
            CastTime = 0,
            School = School.Holy,
            GCD = new GCD() { Duration = 1500, Category = GCDCategory.Spell },
            Effects = new List<SpellEffect>()
            { }
        };

        public static readonly Spell SealOfBlood = new()
        {
            ID = 31892,
            Name = "Seal of Blood",
            ManaCost = 210,
            Cooldown = 0,
            CastTime = 0,
            School = School.Holy,
            GCD = new GCD() { Duration = 1500, Category = GCDCategory.Spell },
            Effects = new List<SpellEffect>()
            { }
        };

        public static readonly Spell SealOfTheCrusader = new()
        {
            ID = 27158,
            Name = "Seal of the Crusader",
            ManaCost = 210,
            Cooldown = 0,
            CastTime = 0,
            School = School.Holy,
            GCD = new GCD() { Duration = 1500, Category = GCDCategory.Spell },
            Effects = new List<SpellEffect>()
            { }
        };

        public static readonly Dictionary<int, Spell> ByID = new()
        {
            { CrusaderStrike.ID, CrusaderStrike },
            { SealOfCommand.ID, SealOfCommand },
            { SealOfBlood.ID, SealOfBlood },
            { SealOfTheCrusader.ID, SealOfTheCrusader }
        };
    }
}

