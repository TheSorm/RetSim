using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
    {
        public static class Auras
        {

            public static Aura SealOfTheCrusader;

            public static Aura SealOfCommand;

            public static Aura SealOfBlood;

            public static Aura DragonspineTrophy;

            public static Dictionary<int, Aura> ByID;

            public static HashSet<Aura> Seals;

            public static void Initialize()
            {
                SealOfTheCrusader = new()
                {
                    ID = 20306,
                    Name = "Seal of the Crusader",
                    Duration = 30 * 1000,
                    MaxStacks = 1,
                    Effects = new List<AuraEffect>()
                    { }
                };

                SealOfCommand = new()
                {
                    ID = 27170,
                    Name = "Seal of Command",
                    Duration = 30 * 1000,
                    MaxStacks = 1                    
                };

                SealOfBlood = new()
                {
                    ID = 31892,
                    Name = "Seal of Blood",
                    Duration = 30 * 1000,
                    MaxStacks = 1,
                    Effects = new List<AuraEffect>()
                    { }
                };

                DragonspineTrophy = new()
                {
                    ID = 34775,
                    Name = "Dragonspine Trophy",
                    Duration = 10 * 1000,
                    MaxStacks = 1,
                    Effects = new List<AuraEffect>()
                    { }
                };

                ByID = new()
                {
                    { SealOfTheCrusader.ID, SealOfTheCrusader },
                    { SealOfCommand.ID, SealOfCommand },
                    { SealOfBlood.ID, SealOfBlood },
                    { DragonspineTrophy.ID, DragonspineTrophy }
                };

                Seals = new()
                {
                    SealOfTheCrusader,
                    SealOfCommand,
                    SealOfBlood
                };
            }

            public static void AddProcs()
            {
                SealOfCommand.Effects = new List<AuraEffect>()
                    { new GainProc(Procs.SealOfCommand) };

                SealOfBlood.Effects = new List<AuraEffect>()
                    { new GainProc(Procs.SealOfBlood) };
            }
        }
    }
}