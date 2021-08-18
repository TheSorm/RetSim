using System.Collections.Generic;

namespace RetSim
{
    public static class Spellbook
    {
        public const int DefaultGCDTime = 1500;
        public enum GCDType
        {
            Physical,
            Spell,
            Non
        }
        public record Spell
        {
            public int ID { get; init; }
            public string Name { get; init; }
            public int Cooldown { get; init; }
            public GCDType GCD { get; init; }
        }

        public static readonly Spell crusaderStrike = new() { ID = 35395, Name = "Crusader Strike", Cooldown = 6000, GCD = GCDType.Physical };
        public static readonly Spell sealOfTheCrusader = new() { ID = 20306, Name = "Seal of the Crusader", Cooldown = 0, GCD = GCDType.Spell };
        public static readonly Spell sealOfCommand = new() { ID = 27170, Name = "Seal of Command", Cooldown = 0, GCD = GCDType.Spell };
        public static readonly Spell sealOfBlood = new() { ID = 31892, Name = "Seal of Blood", Cooldown = 0, GCD = GCDType.Spell };

        public static readonly Dictionary<int, Spell> ByID = new()
        {
            { crusaderStrike.ID, crusaderStrike },
            { sealOfTheCrusader.ID, sealOfTheCrusader },
            { sealOfCommand.ID, sealOfCommand },
            { sealOfBlood.ID, sealOfBlood },
        };
    }
}
