using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
    {
        public static class Auras
        {
            public static readonly Aura SealOfTheCrusader = new()
            {
                ID = 20306,
                Name = "Seal of the Crusader",
                Duration = 30 * 1000,
                MaxStacks = 1,
                Effects = new List<AuraEffect>()
                { }
            };

            public static readonly Aura SealOfCommand = new()
            {
                ID = 27170,
                Name = "Seal of Command",
                Duration = 30 * 1000,
                MaxStacks = 1
            };

            public static readonly Aura SealOfBlood = new()
            {
                ID = 31892,
                Name = "Seal of Blood",
                Duration = 30 * 1000,
                MaxStacks = 1,
                Effects = new List<AuraEffect>()
                { }
            };

            public static readonly Aura DragonspineTrophy = new()
            {
                ID = 34775,
                Name = "Dragonspine Trophy",
                Duration = 10 * 1000,
                MaxStacks = 1,
                Effects = new List<AuraEffect>()
                { }
            };

            public static readonly Dictionary<int, Aura> ByID = new()
            {
                { SealOfTheCrusader.ID, SealOfTheCrusader },
                { SealOfCommand.ID, SealOfCommand },
                { SealOfBlood.ID, SealOfBlood },
                { DragonspineTrophy.ID, DragonspineTrophy }
            };

            public static readonly HashSet<Aura> Seals = new()
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
            }
        }
    }
}