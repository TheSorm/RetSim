using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim.Data
{
    public static partial class Auras
    {
        public static readonly Dictionary<int, Aura> ByID;

        public static readonly HashSet<Seal> Seals = new()
        {
            SealOfTheCrusader,
            SealOfCommand,
            SealOfBlood
        };

        static Auras()
        {
            SealOfCommand.Effects = new List<AuraEffect>() { new GainProc(Procs.SealOfCommand), new GainSeal() };

            SealOfBlood.Effects = new List<AuraEffect>() { new GainProc(Procs.SealOfBlood), new GainSeal() };

            Vengeance.Effects = new List<AuraEffect>() { new GainProc(Procs.Vengeance) };

            //VengeanceProc.Effects = new List<AuraEffect>() { new ModDamageSchool() { Percentage = 5, Schools = new List<School> { School.Physical, School.Holy, }} };

            //Crusade.Effects = new List<AuraEffect>()
            //{ 
            //    new ModDamageCreature() 
            //    {  
            //        Percentage = 5,
            //        Types = new List<CreatureType> { CreatureType.Humanoid, CreatureType.Demon, CreatureType.Undead, CreatureType.Elemental },
            //        Schools = new List<School> { School.Typeless, School.Physical, School.Holy, School.Fire, School.Nature, School.Frost, School.Shadow, School.Arcane }
            //    }
            //};


            TwoHandedWeaponSpecialization.Effects = new List<AuraEffect>()
            {
                new ModDamageSpell() 
                { 
                    Percentage = 6,
                    Spells = new List<Spell> { Spells.Melee, Spells.WindfuryProc, Spells.SealOfCommandProc, Spells.JudgementOfCommand, Spells.SealOfBloodProc, Spells.JudgementOfBlood 
                    }
                }
            };

            //SanctityAura.Effects = new List<AuraEffect>() { new ModDamageSchool() { Percentage = 10, Schools = new List<School> { School.Holy } } };

            //ImprovedSanctityAura.Effects = new List<AuraEffect>() 
            //{ 
            //    new ModDamageSchool() 
            //    { 
            //        Percentage = 2, 
            //        Schools = new List<School> { School.Typeless, School.Physical, School.Holy, School.Fire, School.Nature, School.Frost, School.Shadow, School.Arcane } 
            //    } 
            //};

            WindfuryTotem.Effects = new List<AuraEffect>()
                    { new GainProc(Procs.WindfuryAttack) };

            DragonspineTrophy.Effects = new List<AuraEffect>()
                    { new GainProc(Procs.DragonspineTrophy) };

            SealOfCommand.ExclusiveWith = new List<Seal>()
                { SealOfBlood, SealOfTheCrusader };
            SealOfCommand.Judgement = Spells.JudgementOfCommand;

            SealOfBlood.ExclusiveWith = new List<Seal>()
                { SealOfCommand, SealOfTheCrusader };
            SealOfBlood.Judgement = Spells.JudgementOfBlood;

            SealOfTheCrusader.ExclusiveWith = new List<Seal>()
                { SealOfCommand, SealOfBlood };
            SealOfTheCrusader.Judgement = Spells.JudgementOfCommand;

            ByID = new()
            {
                { Spells.SealOfTheCrusader.ID, SealOfTheCrusader },
                { Spells.SealOfCommand.ID, SealOfCommand },
                { Spells.SealOfBlood.ID, SealOfBlood },
                { Spells.WindfuryTotem.ID, WindfuryTotem },
                { Spells.WindfuryAttack.ID, WindfuryAttack },
                { Spells.AvengingWrath.ID, AvengingWrath },

                { Spells.GreaterBlessingOfMight.ID, GreaterBlessingOfMight },
                { Spells.GreaterBlessingOfKings.ID, GreaterBlessingOfKings },
                { Spells.GreaterBlessingOfWisdom.ID, GreaterBlessingOfWisdom },
                { Spells.BattleShout.ID, BattleShout },
                { Spells.TrueshotAura.ID, TrueshotAura },
                { Spells.FerociousInspiration.ID, FerociousInspiration },

                { Spells.ImprovedSealOfTheCrusader.ID, ImprovedSealOfTheCrusader },
                { Spells.Conviction.ID, Conviction },
                { Spells.Crusade.ID, Crusade },
                { Spells.TwoHandedWeaponSpecialization.ID, TwoHandedWeaponSpecialization },
                { Spells.SanctityAura.ID, SanctityAura },
                { Spells.ImprovedSanctityAura.ID, ImprovedSanctityAura },
                { Spells.SanctifiedSeals.ID, SanctifiedSeals },
                { Spells.Vengeance.ID, Vengeance },
                { Spells.VengeanceProc.ID, VengeanceProc },
                { Spells.Precision.ID, Precision },
                { Spells.DivineStrength.ID, DivineStrength },

                { Spells.DragonspineTrophy.ID, DragonspineTrophy },
                { Spells.DragonspineTrophyProc.ID, DragonspineTrophyProc }
            };
        }
    }
}