using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
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

            public static readonly Aura WindfuryTotem = new()
            {
                Duration = 200000
            };

            public static readonly Aura WindfuryAttack = new()
            {
                Duration = 8,
                Effects = new List<AuraEffect>
                {
                    new GainStats() { Stats = new Stats() { AttackPower = 445 } }
                }
            };

            public static readonly Aura DragonspineTrophy = new()
            {
                Duration = 200000,
            };

            public static readonly Aura DragonspineTrophyProc = new()
            {
                Duration = 10000,
                Effects = new List<AuraEffect> 
                { 
                    new GainStats() { Stats = new Stats() { HasteRating = 325 } } 
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
                    { new GainProc(Procs.SealOfCommand) };

                SealOfBlood.Effects = new List<AuraEffect>()
                    { new GainProc(Procs.SealOfBlood) };

                WindfuryTotem.Effects = new List<AuraEffect>()
                    { new GainProc(Procs.WindfuryAttack) };

                DragonspineTrophy.Effects = new List<AuraEffect>()
                    { new GainProc(Procs.DragonspineTrophy) };

                SealOfCommand.ExclusiveWith = new List<Seal>()
                { SealOfBlood, SealOfTheCrusader };

                SealOfBlood.ExclusiveWith = new List<Seal>()
                { SealOfCommand, SealOfTheCrusader };

                SealOfTheCrusader.ExclusiveWith = new List<Seal>()
                { SealOfCommand, SealOfBlood };

                ByID = new()
                {
                    { Spells.SealOfTheCrusader.ID, SealOfTheCrusader },
                    { Spells.SealOfCommand.ID, SealOfCommand },
                    { Spells.SealOfBlood.ID, SealOfBlood },
                    { Spells.WindfuryTotem.ID, WindfuryTotem },
                    { Spells.WindfuryAttack.ID, WindfuryAttack },
                    { Spells.DragonspineTrophy.ID, DragonspineTrophy }
                };
            }
        }
    }
}