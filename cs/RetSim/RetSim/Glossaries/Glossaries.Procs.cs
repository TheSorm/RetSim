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
                ProcMask = ProcMask.OnAutoAttack | ProcMask.OnWindfury,
                Chance = 100,
                PPM = 7,
                Cooldown = 1000,
            };

            public static readonly Proc SealOfBlood = new()
            {
                ID = 31893,
                Name = "Seal of Blood",
                ProcMask = ProcMask.OnBasicAttack,
                Chance = 100,
                PPM = 0,
                Cooldown = 0
            };

            public static readonly Proc WindfuryAttack = new()
            {
                ID = 25584,
                Name = "Windfury Attack",
                ProcMask = ProcMask.OnAutoAttack,
                Chance = 20,
                PPM = 0,
                Cooldown = 0
            };

            public static readonly Proc DragonspineTrophy = new()
            {
                ID = 34774,
                Name = "Dragonspine Trophy",
                ProcMask = ProcMask.OnAnyAttack,
                Chance = 100,
                PPM = 1,
                Cooldown = 20000
            };

            public static readonly Dictionary<int, Proc> ByID = new()
            {
                { SealOfCommand.ID, SealOfCommand },
                { SealOfBlood.ID, SealOfBlood },
                { WindfuryAttack.ID, WindfuryAttack },
                { DragonspineTrophy.ID, DragonspineTrophy },
            };

            static Procs()
            {
                SealOfCommand.Spell = Spells.SealOfCommandProc;
                SealOfBlood.Spell = Spells.SealOfBloodProc;
                WindfuryAttack.Spell = Spells.WindfuryAttack;
                DragonspineTrophy.Spell = Spells.DragonspineTrophy;
            }
        }
    }
}
