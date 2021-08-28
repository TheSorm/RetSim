using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
    {
        public static class Spells
        {
            public static readonly Spell CrusaderStrike = new()
            {
                ID = 35395,
                Name = "Crusader Strike",
                ManaCost = 236,
                Cooldown = 6000,
                CastTime = 0,
                School = School.Physical,
                HitCheck = HitCheck.Special,
                GCD = new GCD() { Duration = 1500, Category = GCDCategory.Physical },
            };

            public static readonly Spell SealOfCommand = new()
            {
                ID = 27170,
                Name = "Seal of Command (Aura)",
                ManaCost = 280,
                Cooldown = 0,
                CastTime = 0,
                School = School.Holy,
                GCD = new GCD() { Duration = 1500, Category = GCDCategory.Spell },
            };

            public static readonly Spell SealOfCommandProc = new()
            {
                ID = 20424,
                Name = "Seal of Command",
                ManaCost = 0,
                Cooldown = 1 * 1000,
                CastTime = 0,
                School = School.Holy,
                HitCheck = HitCheck.Special,
                GCD = new GCD() { Duration = 0, Category = GCDCategory.None },
            };

            public static readonly Spell SealOfBlood = new()
            {
                ID = 31892,
                Name = "Seal of Blood (Aura)",
                ManaCost = 210,
                Cooldown = 0,
                CastTime = 0,
                School = School.Holy,
                GCD = new GCD() { Duration = 1500, Category = GCDCategory.Spell },
            };

            public static readonly Spell SealOfBloodProc = new()
            {
                ID = 31893,
                Name = "Seal of Blood",
                ManaCost = 0,
                Cooldown = 0,
                CastTime = 0,
                School = School.Holy,
                HitCheck = HitCheck.Special,
                GCD = new GCD() { Duration = 0, Category = GCDCategory.None },
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

            public static readonly Spell DragonspineTrophy = new()
            {
                ID = 34775,
                Name = "Dragonspine Trophy",
                ManaCost = 0,
                Cooldown = 20 * 1000,
                CastTime = 0,
                School = School.None,
                GCD = new GCD() { Duration = 0, Category = GCDCategory.None },
            };

            public static readonly Dictionary<int, Spell> ByID = new()
            {
                { CrusaderStrike.ID, CrusaderStrike },
                { SealOfCommand.ID, SealOfCommand },
                { SealOfCommandProc.ID, SealOfCommandProc },
                { SealOfBlood.ID, SealOfBlood },
                { SealOfBloodProc.ID, SealOfBloodProc },
                { SealOfTheCrusader.ID, SealOfTheCrusader },
                { DragonspineTrophy.ID, DragonspineTrophy }
            };
            static Spells()
            {
                CrusaderStrike.Effects = new List<SpellEffect>()
                    { new NormalizedWeaponDamage(1.1f) };
                SealOfCommand.Effects = new List<SpellEffect>()
                    { new ChangeSeal(Auras.SealOfCommand) };
                SealOfCommandProc.Effects = new List<SpellEffect>()
                    { new NormalizedWeaponDamage(0.7f) };
                SealOfBlood.Effects = new List<SpellEffect>()
                    { new ChangeSeal(Auras.SealOfBlood) };
                SealOfBloodProc.Effects = new List<SpellEffect>()
                    { new SealOfBlood() };
                DragonspineTrophy.Effects = new List<SpellEffect>()
                    { new GainAura(Auras.DragonspineTrophy, false) };

            }
        }
    }
}