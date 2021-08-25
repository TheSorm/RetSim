
using System;
using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
    {
        public static class Procs
        {
            public static Proc SealOfCommand = null;

            public static Proc SealOfBlood = null;

            public static Proc MagtheridonMeleeTrinket = null;

            public static Dictionary<int, Proc> ByID;

            public static void Initialize()
            {
                SealOfCommand = new()
                {
                    ID = 27170,
                    Name = "Seal of Command",
                    ProcMask = ProcMask.OnMeleeAutoAttack,
                    Spell = Spells.SealOfCommandProc,
                    Chance = 100,
                    PPM = 7
                    //Cooldown = 1000,
                };

                SealOfBlood = new()
                {
                    ID = 31893,
                    Name = "Seal of Blood",
                    ProcMask = ProcMask.OnMeleeAutoAttack,
                    Spell = Spells.SealOfBloodProc,
                    Chance = 100,
                    PPM = 0
                    //Cooldown = 0
                };

                MagtheridonMeleeTrinket = new()
                {
                    ID = 34774,
                    Name = "Magtheridon Melee Trinket",
                    ProcMask = ProcMask.OnMeleeAutoAttack | ProcMask.OnMeleeSpecialAttack,
                    Spell = Spells.DragonspineTrophy,
                    Chance = 100,
                    PPM = 1
                    //Cooldown = 20 * 1000,
                };

                ByID = new()
                {
                    { SealOfCommand.ID, SealOfCommand },
                    { SealOfBlood.ID, SealOfBlood },
                    { MagtheridonMeleeTrinket.ID, MagtheridonMeleeTrinket },
                };
            }
        }
    }
}
