using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim.Data
{
    public static class Auras
    {
        public static readonly Seal SealOfCommand = new()
        {
            Persist = 400
        };

        public static readonly Seal SealOfBlood = new()
        {
        };

        public static readonly Seal SealOfTheCrusader = new()
        {
        };

        public static readonly Aura Vengeance = new()
        {
        };

        public static readonly Aura VengeanceProc = new()
        {
            Duration = 30000,
            MaxStacks = 3            
        };

        public static readonly Aura WindfuryTotem = new()
        {
        };

        public static readonly Aura WindfuryAttack = new()
        {
            Duration = 10,
            Effects = new List<AuraEffect>
                {
                    new GainStats() { Stats = new StatSet() { AttackPower = 445 } }
                }
        };

        public static readonly Aura DragonspineTrophy = new()
        {
        };

        public static readonly Aura DragonspineTrophyProc = new()
        {
            Duration = 10000,
            Effects = new List<AuraEffect>
                {
                    new GainStats() { Stats = new StatSet() { HasteRating = 325 } }
                }
        };

        public static readonly Dictionary<int, Aura> ByID;

        public static readonly HashSet<Seal> Seals = new()
        {
            SealOfTheCrusader,
            SealOfCommand,
            SealOfBlood
        };

        static Auras()
        {
            SealOfCommand.Effects = new List<AuraEffect>()
                    { new GainProc(Procs.SealOfCommand), new GainSeal() };

            SealOfBlood.Effects = new List<AuraEffect>()
                    { new GainProc(Procs.SealOfBlood), new GainSeal() };

            Vengeance.Effects = new List<AuraEffect>()
                     { new GainProc(Procs.Vengeance) };

            VengeanceProc.Effects = new List<AuraEffect>()
                    { new ModDamage() { Schools = new List<School> { School.Holy, School.Physical }, Percentage = 5 } };

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
                { Spells.Vengeance.ID, Vengeance },
                { Spells.VengeanceProc.ID, VengeanceProc },
                { Spells.WindfuryTotem.ID, WindfuryTotem },
                { Spells.WindfuryAttack.ID, WindfuryAttack },
                { Spells.DragonspineTrophy.ID, DragonspineTrophy },
                { Spells.DragonspineTrophyProc.ID, DragonspineTrophyProc }
            };
        }
    }
}