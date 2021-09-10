using System.Collections.Generic;

namespace RetSim
{
    public static class Races
    {
        public static readonly Race Human = new()
        {
            Name = "Human",
            Stats = new()
            {
                [StatName.Strength] = 126,
                [StatName.Agility] = 77,
                [StatName.Intellect] = 83,
                [StatName.Stamina] = 120
            }
        };

        public static readonly Race Dwarf = new()
        {
            Name = "Dwarf",
            Stats = new()
            {
                [StatName.Strength] = 128,
                [StatName.Agility] = 73,
                [StatName.Intellect] = 82,
                [StatName.Stamina] = 123
            }
        };

        public static readonly Race Draenei = new()
        {
            Name = "Draenei",
            Stats = new()
            {
                [StatName.Strength] = 127,
                [StatName.Agility] = 74,
                [StatName.Intellect] = 84,
                [StatName.Stamina] = 119
            }
        };

        public static readonly Race BloodElf = new()
        {
            Name = "Blood Elf",
            Stats = new()
            {
                [StatName.Strength] = 123,
                [StatName.Agility] = 79,
                [StatName.Intellect] = 87,
                [StatName.Stamina] = 118
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

    public class Race
    {
        public string Name { get; init; }
        public StatSet Stats { get; init; }
    }
}