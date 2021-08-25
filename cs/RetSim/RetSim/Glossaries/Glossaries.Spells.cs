using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
    {
        public static class Spells
        {
            public static Spell CrusaderStrike;

            public static Spell SealOfCommand;

            public static Spell SealOfCommandProc;

            public static Spell SealOfBlood;

            public static Spell SealOfBloodProc;

            public static Spell SealOfTheCrusader;

            public static Spell DragonspineTrophy;

            public static Dictionary<int, Spell> ByID;
            public static void Initialize()
            {
                CrusaderStrike = new()
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

                SealOfCommand = new()
                {
                    ID = 27170,
                    Name = "Seal of Command",
                    ManaCost = 280,
                    Cooldown = 0,
                    CastTime = 0,
                    School = School.Holy,
                    GCD = new GCD() { Duration = 1500, Category = GCDCategory.Spell },
                    Effects = new List<SpellEffect>()
                    { new GainAura(Auras.SealOfCommand, true) }
                };

                SealOfCommandProc = new()
                {
                    ID = 20424,
                    Name = "Seal of Command DAMAGE",
                    ManaCost = 0,
                    Cooldown = 1 * 1000,
                    CastTime = 0,
                    School = School.Holy,
                    GCD = new GCD() { Duration = 0, Category = GCDCategory.None },
                    Effects = new List<SpellEffect>()
                    { new NormalizedWeaponDamage(0.7f) }
                };

                SealOfBlood = new()
                {
                    ID = 31892,
                    Name = "Seal of Blood",
                    ManaCost = 210,
                    Cooldown = 0,
                    CastTime = 0,
                    School = School.Holy,
                    GCD = new GCD() { Duration = 1500, Category = GCDCategory.Spell },
                    Effects = new List<SpellEffect>()
                    { new GainAura(Auras.SealOfBlood, true) }
                };

                SealOfBloodProc = new()
                {
                    ID = 31893,
                    Name = "Seal of Blood DAMAGE",
                    ManaCost = 0,
                    Cooldown = 0,
                    CastTime = 0,
                    School = School.Holy,
                    GCD = new GCD() { Duration = 0, Category = GCDCategory.None },
                    Effects = new List<SpellEffect>()
                    { new SealOfBlood() }
                };

                SealOfTheCrusader = new()
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

                DragonspineTrophy = new()
                {
                    ID = 34775,
                    Name = "Dragonspine Trophy",
                    ManaCost = 0,
                    Cooldown = 20 * 1000,
                    CastTime = 0,
                    School = School.None,
                    GCD = new GCD() { Duration = 0, Category = GCDCategory.None },
                    Effects = new List<SpellEffect>()
                    { new GainAura(Auras.DragonspineTrophy, false) }
                };

                ByID = new()
                {
                    { CrusaderStrike.ID, CrusaderStrike },
                    { SealOfCommand.ID, SealOfCommand },
                    { SealOfCommandProc.ID, SealOfCommandProc },
                    { SealOfBlood.ID, SealOfBlood },
                    { SealOfBloodProc.ID, SealOfBloodProc },
                    { SealOfTheCrusader.ID, SealOfTheCrusader },
                    { DragonspineTrophy.ID, DragonspineTrophy }
                };
            }
        }
    }
}