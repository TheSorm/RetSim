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
        public School School { get; init; }
        public GCD GCD { get; init; }
        public float Coefficient { get; init; }
        public HitCheck HitCheck { get; init; }
        public List<SpellEffect> Effects { get; set; }
    }

    public record GCD
    {
        public int Duration { get; init; }
        public GCDCategory Category { get; init; }
    }

    public enum GCDCategory
    {
        None = 0,
        Physical = 1,
        Spell = 2
    }

    public enum School
    {
        None = 0,
        Physical = 1,
        Holy = 2,
        Fire = 3,
        Nature = 4,
        Frost = 5,
        Shadow = 6,
        Arcane = 7
    }

    public enum HitCheck
    {
        None = 0,
        Auto = 1,
        Special = 2,
        Ranged = 3,
        Magic = 4
    }
}
