using System.Collections.Generic;

namespace RetSim
{
    public static class Spellbook
    {
        public static readonly Spell crusaderStrike = new() { ID = 35395, Name = "Crusader Strike" , Cooldown = 6000};

        public static readonly Dictionary<int, Spell> ByID = new()
        {
            { crusaderStrike.ID, crusaderStrike},
        };
    }
}
