using System.Collections.Generic;

namespace RetSim
{
    public static class Auras
    {
        public record Aura
        {
            public int ID { get; init; }
            public string Name { get; init; }
            public int Duration { get; init; }
        }

        public static readonly Aura sealOfTheCrusader = new() { ID = 20306, Name = "Seal of the Crusader", Duration = 30 * 1000 };

        public static readonly Dictionary<int, Aura> ByID = new()
        {
            { sealOfTheCrusader.ID, sealOfTheCrusader },
        };
    }
}
