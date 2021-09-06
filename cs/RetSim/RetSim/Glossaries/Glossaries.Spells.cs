using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
    {
        public static class Spells
        {
            public static readonly Spell Melee = new()
            {
                ID = 1,
                Name = "Melee",
                ManaCost = 0,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 0, Category = Category.None },
            };

            public static readonly Spell CrusaderStrike = new()
            {
                ID = 35395,
                Name = "Crusader Strike",
                ManaCost = 236,
                Cooldown = 6000,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 1500, Category = Category.Physical },
            };

            public static readonly Spell SealOfCommand = new()
            {
                ID = 27170,
                Name = "Seal of Command (Aura)",
                ManaCost = 280,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 1500, Category = Category.Spell },
            };

            public static readonly Spell SealOfCommandProc = new()
            {
                ID = 20424,
                Name = "Seal of Command",
                ManaCost = 0,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 0, Category = Category.None },
            };

            public static readonly Spell SealOfBlood = new()
            {
                ID = 31892,
                Name = "Seal of Blood (Aura)",
                ManaCost = 210,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 1500, Category = Category.Spell },
            };

            public static readonly Spell SealOfBloodProc = new()
            {
                ID = 31893,
                Name = "Seal of Blood",
                ManaCost = 0,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 0, Category = Category.None },
            };

            public static readonly Spell SealOfTheCrusader = new()
            {
                ID = 27158,
                Name = "Seal of the Crusader",
                ManaCost = 210,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 1500, Category = Category.Spell },
                Effects = new List<SpellEffect>()
                { }
            };

            public static readonly Spell WindfuryTotem = new()
            {
                ID = 25580,
                Name = "Windfury Totem",
                ManaCost = 0,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 0, Category = Category.None },
            };

            public static readonly Spell WindfuryAttack = new()
            {
                ID = 25584,
                Name = "Windfury Attack",
                ManaCost = 0,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 0, Category = Category.None },
            };

            public static readonly Spell WindfuryProc = new()
            {
                ID = 2,
                Name = "Melee (Windfury)",
                ManaCost = 0,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 0, Category = Category.None },
            };

            public static readonly Spell DragonspineTrophy = new()
            {
                ID = 34775,
                Name = "Dragonspine Trophy",
                ManaCost = 0,
                Cooldown = 0,
                CastTime = 0,
                GCD = new SpellGCD() { Duration = 0, Category = Category.None },
            };

            public static readonly Dictionary<int, Spell> ByID = new()
            {
                { Melee.ID, Melee },
                { CrusaderStrike.ID, CrusaderStrike },
                { SealOfCommand.ID, SealOfCommand },
                { SealOfCommandProc.ID, SealOfCommandProc },
                { SealOfBlood.ID, SealOfBlood },
                { SealOfBloodProc.ID, SealOfBloodProc },
                { SealOfTheCrusader.ID, SealOfTheCrusader },
                { WindfuryTotem.ID, WindfuryTotem },
                { WindfuryAttack.ID, WindfuryAttack },
                { WindfuryProc.ID, WindfuryProc },
                { DragonspineTrophy.ID, DragonspineTrophy }
            };
            static Spells()
            {
                Melee.Effects = new List<SpellEffect>()
                {
                    new WeaponDamage()
                    {
                        Spell = Melee,
                        School = School.Physical,
                        DefenseCategory = DefenseType.Auto,
                        CritCategory = Category.Physical,
                        Normalized = false,
                        OnCast = ProcMask.None,
                        OnHit = ProcMask.OnAutoAttack,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit,
                        Percentage = 1f,
                        Coefficient = 0,
                        HolyCoefficient = 0
                    }
                };

                CrusaderStrike.Effects = new List<SpellEffect>()
                {
                    new WeaponDamage()
                    {
                        Spell = CrusaderStrike,
                        School = School.Physical,
                        DefenseCategory = DefenseType.Special,
                        CritCategory = Category.Physical,
                        Normalized = true,
                        OnCast = ProcMask.None,
                        OnHit = ProcMask.OnSpecialAttack,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit,
                        Percentage = 1.1f,
                        Coefficient = 0,
                        HolyCoefficient = 0
                    }
                };

                SealOfCommand.Effects = new List<SpellEffect>()
                { new SealChange(Auras.SealOfCommand) };

                SealOfCommandProc.Effects = new List<SpellEffect>()
                {
                    new WeaponDamage()
                    {
                        Spell = SealOfCommandProc,
                        School =  School.Holy,
                        DefenseCategory = DefenseType.Special,
                        CritCategory = Category.Physical,
                        Normalized = false,
                        OnCast = ProcMask.None,
                        OnHit = ProcMask.OnSpecialAttack | ProcMask.OnSealOfCommand,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit,
                        Percentage = 0.7f,
                        Coefficient = 0.2f,
                        HolyCoefficient = 0.29f
                    }
                };

                SealOfBlood.Effects = new List<SpellEffect>()
                { new SealChange(Auras.SealOfBlood) };

                SealOfBloodProc.Effects = new List<SpellEffect>()
                {
                    new WeaponDamage()
                    {
                        Spell = SealOfBloodProc,
                        School = School.Holy,
                        DefenseCategory = DefenseType.Special,
                        CritCategory = Category.Physical,
                        Normalized = false,
                        OnCast = ProcMask.None,
                        OnHit = ProcMask.None,
                        OnCrit = ProcMask.OnAnyCrit,
                        Percentage = 0.35f,
                        Coefficient = 0,
                        HolyCoefficient = 0
                    }
                };

                WindfuryTotem.Effects = new List<SpellEffect>()
                {
                    { new GainAura(Auras.WindfuryTotem, false) }
                };

                WindfuryAttack.Effects = new List<SpellEffect>()
                {
                     { new GainVariableDurationAura(Auras.WindfuryAttack, false, 0, 9) },
                    { new ExtraAttacks(WindfuryProc, 1) { Spell = WindfuryAttack } }
                   
                };

                WindfuryProc.Effects = new List<SpellEffect>()
                {
                    new WeaponDamage()
                    {
                        Spell = WindfuryProc,
                        School =  School.Physical,
                        DefenseCategory = DefenseType.Auto,
                        CritCategory = Category.Physical,
                        Normalized = false,
                        OnCast = ProcMask.None,
                        OnHit = ProcMask.OnWindfury,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit,
                        Percentage = 0.7f,
                        Coefficient = 0.2f,
                        HolyCoefficient = 0.29f
                    }
                };

                DragonspineTrophy.Effects = new List<SpellEffect>()
                    { new GainAura(Auras.DragonspineTrophy, false) };

            }
        }
    }
}