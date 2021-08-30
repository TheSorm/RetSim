using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public record Spell
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int ManaCost { get; init; }
        public int Cooldown { get; init; }
        public int CastTime { get; init; }
        public SpellGCD GCD { get; init; }
        public List<SpellEffect> Effects { get; set; }
    }

    public record SpellGCD
    {
        public int Duration { get; init; }
        public Category Category { get; init; }
    }

    public enum Category
    {
        None = 0,
        Physical = 1,
        Spell = 2
    }

    public enum School
    {
        Typeless = 0,
        Physical = 1,
        Holy = 2,
        Fire = 3,
        Nature = 4,
        Frost = 5,
        Shadow = 6,
        Arcane = 7
    }

    public enum DefenseType
    {
        None = 0,
        Auto = 1,
        Special = 2,
        Ranged = 3,
        Magic = 4
    }
}
