using System.Collections.Generic;

namespace RetSim
{
    public static class Races
    {
        public static Race Human = new Race
        {
            Name = "Human",
            Stats = new RaceStats
            {
                Strength = 126,
                Agility = 77,
                Intellect = 83,
                Stamina = 120
            }
        };

        public static Race Dwarf = new Race
        {
            Name = "Dwarf",
            Stats = new RaceStats
            {
                Strength = 128,
                Agility = 73,
                Intellect = 82,
                Stamina = 123
            }
        };

        public static Race Draenei = new Race
        {
            Name = "Draenei",
            Stats = new RaceStats
            {
                Strength = 127,
                Agility = 74,
                Intellect = 84,
                Stamina = 119
            }
        };

        public static Race BloodElf = new Race
        {
            Name = "Blood Elf",
            Stats = new RaceStats
            {
                Strength = 123,
                Agility = 79,
                Intellect = 87,
                Stamina = 118
            }
        };

        public static Dictionary<string, Race> ByName = new Dictionary<string, Race>()
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
        public RaceStats Stats { get; init; }
    }

    public record RaceStats
    {
        public int Strength { get; init; }
        public int Agility { get; init; }
        public int Intellect { get; init; }
        public int Stamina { get; init; }
    }

    /*
     *  Race Independent Base Stats
     *
     *  Health = 3197,
     *  Mana = 2673,
     *
     *  AttackPower = 190,
     *  CritChance = 0.6f,
     *
     *  SpellCritChance = 3.32f
     */
}