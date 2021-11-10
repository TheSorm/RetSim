using Newtonsoft.Json.Converters;
using RetSim.Misc;
using RetSim.Spells.SpellEffects;
using RetSim.Units.Player;
using Newtonsoft.Json;

namespace RetSim.Spells;

[Serializable]
public class Spell
{
    public int ID { get; init; }
    public string Name { get; init; }
    public int Rank { get; init; } = 0;
    
    public int ManaCost { get; init; } = 0;
    public int Cooldown { get; init; } = 0;
    public int CastTime { get; init; } = 0;
    public SpellGCD GCD { get; init; } = null;

    [JsonConverter(typeof(StringEnumConverter))]
    public SpellTarget Target { get; init; } = SpellTarget.Self;

    public Aura Aura { get; set; } = null;
    public List<SpellEffect> Effects { get; set; } = null;
    
    [JsonIgnore]
    public Func<Player, bool> Requirements { get; init; }

    public override string ToString()
    {
        return $"{Name} (Rank {Rank}) (ID: {ID})";
    }
}

public class Judgement : Spell { }

public class Talent : Spell { }

public class Racial : Spell { }

public enum SpellTarget
{
    Self = 0,
    Enemy = 1,
    Ally = 2
}

public class SpellGCD
{
    public int Duration { get; init; } = Constants.Numbers.DefaultGCD;

    [JsonConverter(typeof(StringEnumConverter))]
    public AttackCategory Category { get; init; } = AttackCategory.Physical;
}

[Flags]
public enum School
{
    Typeless = 0,
    Physical = 1,
    Holy = 2,
    Fire = 4,
    Nature = 8,
    Frost = 16,
    Shadow = 32,
    Arcane = 64,
    Magic = Holy | Fire | Nature | Frost | Shadow | Arcane,
    All = Physical | Holy | Fire | Nature | Frost | Shadow | Arcane
}

public enum AttackCategory
{
    Physical = 1,
    Spell = 2
}

public enum DefenseType
{
    None = 0,
    White = 1,
    Special = 2,
    Ranged = 3,
    Magic = 4
}