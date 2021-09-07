using RetSim.SpellEffects;
using System.Collections.Generic;
using System.Linq;

namespace RetSim.Data
{
    
        public static class Spells
        {
            public static readonly Spell Melee = new()
            {
                ID = 1,
                Name = "Melee",

                Effects = new List<SpellEffect>
                {
                    new WeaponDamage()
                    {
                        School = School.Physical,
                        DefenseCategory = DefenseType.White,
                        CritCategory = AttackCategory.Physical,
                        OnHit = ProcMask.OnAutoAttack,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit
                    }
                }
            };

            public static readonly Spell CrusaderStrike = new()
            {
                ID = 35395,
                Name = "Crusader Strike",
                ManaCost = 236,
                Cooldown = 6000,
                GCD = new SpellGCD() { Category = AttackCategory.Physical },

                Effects = new List<SpellEffect>
                {
                    new WeaponDamage()
                    {
                        School = School.Physical,
                        DefenseCategory = DefenseType.Special,
                        CritCategory = AttackCategory.Physical,
                        Normalized = true,
                        OnHit = ProcMask.OnSpecialAttack,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit,
                        Percentage = 1.1f
                    }
                }
            };

            public static readonly Spell Judgement = new()
            {
                ID = 20271,
                Name = "Judgement",
                ManaCost = 148,
                Cooldown = 10000,

                Effects = new List<SpellEffect> { new JudgementEffect() }
            };

            public static readonly Spell SealOfCommand = new()
            {
                ID = 27170,
                Name = "Seal of Command (Aura)",
                ManaCost = 280,
                GCD = new SpellGCD() { Category = AttackCategory.Spell }
            };

            public static readonly Spell SealOfCommandProc = new()
            {
                ID = 20424,
                Name = "Seal of Command",

                Effects = new List<SpellEffect>
                {
                    new WeaponDamage()
                    {
                        School =  School.Holy,
                        DefenseCategory = DefenseType.Special,
                        CritCategory = AttackCategory.Physical,
                        OnHit = ProcMask.OnSpecialAttack | ProcMask.OnSealOfCommand,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit,
                        Percentage = 0.7f,
                        Coefficient = 0.2f,
                        HolyCoefficient = 0.29f
                    }
                }
            };

            public static readonly Judgement JudgementOfCommand = new()
            {
                ID = 27171,
                Name = "Judgement of Command",

                Effects = new List<SpellEffect>
                {
                    new DamageEffect
                    {
                        School =  School.Holy,
                        DefenseCategory = DefenseType.Ranged,
                        CritCategory = AttackCategory.Physical,
                        OnHit = ProcMask.OnSpecialAttack,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit,
                        Coefficient = 0.429f,
                        MinEffect = 228,
                        MaxEffect = 252
                    }
                }
            };

            public static readonly Spell SealOfBlood = new()
            {
                ID = 31892,
                Name = "Seal of Blood (Aura)",
                ManaCost = 210,
                GCD = new SpellGCD() { Category = AttackCategory.Spell }
            };

            public static readonly Spell SealOfBloodProc = new()
            {
                ID = 31893,
                Name = "Seal of Blood",

                Effects = new List<SpellEffect>
                {
                    new WeaponDamage()
                    {
                        School = School.Holy,
                        DefenseCategory = DefenseType.Special,
                        CritCategory = AttackCategory.Physical,
                        OnCrit = ProcMask.OnAnyCrit,
                        Percentage = 0.35f
                    }
                }
            };
            
            public static readonly Judgement JudgementOfBlood = new()
            {
                ID = 31898,
                Name = "Judgement of Blood",

                Effects = new List<SpellEffect>
                {
                    new DamageEffect
                    {
                        School =  School.Holy,
                        DefenseCategory = DefenseType.Ranged,
                        CritCategory = AttackCategory.Physical,
                        OnHit = ProcMask.OnSpecialAttack,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit,
                        Coefficient = 0.429f,
                        MinEffect = 331.6f,
                        MaxEffect = 361.6f
                    }
                }
            };

            public static readonly Spell SealOfTheCrusader = new()
            {
                ID = 27158,
                Name = "Seal of the Crusader",
                ManaCost = 210,
                GCD = new SpellGCD() { Category = AttackCategory.Spell },

                Effects = null
                //TODO: Give this shit an effect
            };

            public static readonly Spell WindfuryTotem = new()
            {
                ID = 25580,
                Name = "Windfury Totem"
            };

            public static readonly Spell WindfuryAttack = new()
            {
                ID = 25584,
                Name = "Windfury Attack"                
            };

            public static readonly Spell WindfuryProc = new()
            {
                ID = 2,
                Name = "Melee (Windfury)",

                Effects = new List<SpellEffect>()
                {
                    new WeaponDamage()
                    {
                        School =  School.Physical,
                        DefenseCategory = DefenseType.White,
                        CritCategory = AttackCategory.Physical,
                        OnHit = ProcMask.OnWindfury,
                        OnCrit = ProcMask.OnAnyCrit | ProcMask.OnMeleeCrit
                    }
                }
            };

            public static readonly Spell DragonspineTrophy = new()
            {
                ID = 34774,
                Name = "Dragonspine Trophy"
            };

            public static readonly Spell DragonspineTrophyProc = new()
            {
                ID = 34775,
                Name = "Dragonspine Trophy"
            };

            private static void AssignChildren(Spell spell)
            {
                if (spell.Aura != null)
                    spell.Aura.Parent = spell;

                if (spell.Effects != null)
                {
                    foreach (SpellEffect effect in spell.Effects)
                    {
                        effect.Parent = spell;
                    }
                }
            }

            private static void AssignAura(Spell spell)
            {
                if (Auras.ByID.ContainsKey(spell.ID))
                    spell.Aura = Auras.ByID[spell.ID];
            }

            public static readonly Dictionary<int, Spell> ByID = new()
            {
                { Melee.ID, Melee },
                { CrusaderStrike.ID, CrusaderStrike },
                { Judgement.ID, Judgement },
                { SealOfCommandProc.ID, SealOfCommandProc },
                { SealOfCommand.ID, SealOfCommand },
                { JudgementOfCommand.ID, JudgementOfCommand },
                { SealOfBloodProc.ID, SealOfBloodProc },
                { SealOfBlood.ID, SealOfBlood },
                { JudgementOfBlood.ID, JudgementOfBlood },
                { SealOfTheCrusader.ID, SealOfTheCrusader },
                { WindfuryProc.ID, WindfuryProc },
                { WindfuryAttack.ID, WindfuryAttack },
                { WindfuryTotem.ID, WindfuryTotem },
                { DragonspineTrophyProc.ID, DragonspineTrophyProc },
                { DragonspineTrophy.ID, DragonspineTrophy }
            };

            static Spells()
            {
                ByID.OrderBy(key => key.Key);

                WindfuryAttack.Effects = new List<SpellEffect> { new ExtraAttacks(WindfuryProc, 1) };

                foreach (Spell spell in ByID.Values)
                {
                    AssignAura(spell);
                    AssignChildren(spell);                    
                }
            }
        }
    
}