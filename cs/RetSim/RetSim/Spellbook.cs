using System.Collections.Generic;

namespace RetSim
{
    public static class Spellbook
    {
        public record Spell
        {
            public int ID { get; init; }
            public string Name { get; init; }
            public int Cooldown { get; init; }
        }

        public static readonly Spell crusaderStrike = new() { ID = 35395, Name = "Crusader Strike", Cooldown = 6000 };
        public static readonly Spell sealOfTheCrusader = new() { ID = 20306, Name = "Seal of the Crusader", Cooldown = 0 };

        public static readonly Dictionary<int, Spell> ByID = new()
        {
            { crusaderStrike.ID, crusaderStrike },
            { sealOfTheCrusader.ID, sealOfTheCrusader },
        };
    }
}
