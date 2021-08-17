using System.Collections.Generic;

namespace RetSim
{
    internal static class Spellbook
    {
        public static readonly Spell crusaderStrike = new() { ID = 35395, Name = "Crusader Strike" , Cooldown = 6000};

        public static readonly Dictionary<int, Spell> byID = new()
        {
            { crusaderStrike.ID, crusaderStrike},
        };
    }
}
