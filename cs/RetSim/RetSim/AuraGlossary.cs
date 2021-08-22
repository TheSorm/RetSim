using System.Collections.Generic;

namespace RetSim
{
    public static class AuraGlossary
    {
        public static readonly Aura SealOfTheCrusader = new()
        {
            ID = 20306,
            Name = "Seal of the Crusader",
            Duration = 30 * 1000,
            Effects = new List<AuraEffect>()
            { }
        };

        public static readonly Aura SealOfCommand = new()
        {
            ID = 27170,
            Name = "Seal of Command",
            Duration = 30 * 1000,
            Effects = new List<AuraEffect>()
            { }
        };

        public static readonly Aura SealOfBlood = new()
        {
            ID = 31892,
            Name = "Seal of Blood",
            Duration = 30 * 1000,
            Effects = new List<AuraEffect>()
            { }
        };

        public static readonly Dictionary<int, Aura> ByID = new()
        {
            { SealOfTheCrusader.ID, SealOfTheCrusader },
            { SealOfCommand.ID, SealOfCommand },
            { SealOfBlood.ID, SealOfBlood },
        };

        public static readonly HashSet<Aura> Seals = new()
        {
            SealOfTheCrusader,
            SealOfCommand,
            SealOfBlood
        };
    }
    public record Aura
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int Duration { get; init; }
        public List<AuraEffect> Effects { get; init; }
    }
}