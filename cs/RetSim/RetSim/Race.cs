using System.Collections.Generic;

namespace RetSim
{
    public static class Races
    {
        public static readonly Race Human = new()
        {
            Name = "Human",
            Stats = new Stats
            {
                Strength = 126,
                Agility = 77,
                Intellect = 83,
                Stamina = 120
            }
        };

        public static readonly Race Dwarf = new()
        {
            Name = "Dwarf",
            Stats = new Stats
            {
                Strength = 128,
                Agility = 73,
                Intellect = 82,
                Stamina = 123
            }
        };

        public static readonly Race Draenei = new()
        {
            Name = "Draenei",
            Stats = new Stats
            {
                Strength = 127,
                Agility = 74,
                Intellect = 84,
                Stamina = 119
            }
        };

        public static readonly Race BloodElf = new()
        {
            Name = "Blood Elf",
            Stats = new Stats
            {
                Strength = 123,
                Agility = 79,
                Intellect = 87,
                Stamina = 118
            }
        };

        public static readonly Dictionary<string, Race> ByName = new()
        {
            { Human.Name, Human },
            { Dwarf.Name, Dwarf },
            { Draenei.Name, Draenei },
            { BloodElf.Name, BloodElf }
        };
    }

    public record Race
    {
        public string Name { get; init; }
        public Stats Stats { get; init; }
    }
}