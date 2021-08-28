using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
    {
        public static class Procs
        {
            public static readonly Proc SealOfCommand = new()
            {
                ID = 27170,
                Name = "Seal of Command",
                ProcMask = ProcMask.OnMeleeAutoAttack,
                Chance = 100,
                PPM = 15
                //Cooldown = 1000,
            };

            public static readonly Proc SealOfBlood = new()
            {
                ID = 31893,
                Name = "Seal of Blood",
                ProcMask = ProcMask.OnMeleeAutoAttack,
                Chance = 100,
                PPM = 0
                //Cooldown = 0
            };

            public static readonly Proc DragonspineTrophy = new()
            {
                ID = 34774,
                Name = "Dragonspine Trophy",
                ProcMask = ProcMask.OnMeleeAutoAttack | ProcMask.OnMeleeSpecialAttack,
                Chance = 100,
                PPM = 1
                //Cooldown = 20 * 1000,
            };

            public static readonly Dictionary<int, Proc> ByID = new()
            {
                { SealOfCommand.ID, SealOfCommand },
                { SealOfBlood.ID, SealOfBlood },
                { DragonspineTrophy.ID, DragonspineTrophy },
            };

            static Procs()
            {
                SealOfCommand.Spell = Spells.SealOfCommandProc;
                SealOfBlood.Spell = Spells.SealOfBloodProc;
                DragonspineTrophy.Spell = Spells.DragonspineTrophy;
            }
        }
    }
}
