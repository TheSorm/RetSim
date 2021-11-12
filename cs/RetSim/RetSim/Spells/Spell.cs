using RetSim.Misc;
using RetSim.Spells.SpellEffects;
using RetSim.Units.Player;
using Newtonsoft.Json;

namespace RetSim.Spells;

public class Spell
{
    public int ID { get; init; }
    public string Name { get; init; }
    public int Rank { get; init; }
    
    public int ManaCost { get; init; }
    public int Cooldown { get; init; }
    public int CastTime { get; init; }
    public SpellGCD GCD { get; init; }

    public SpellTarget Target { get; init; }

    public Aura Aura { get; set; }
    public List<SpellEffect> Effects { get; set; }
    
    [JsonIgnore]
    public Func<Player, bool> Requirements { get; set; }

    public static List<Spell> GetSpells(params int[] spells)
    {
        List<Spell> results = new();

        foreach (int spell in spells)
        {
            if (Data.Collections.Spells[spell] is Spell s)
                results.Add(s);

            else
                throw new Exception($"The given spell with ID {spell} was not a valid spell.");
        }

        return results;
    }

    public override string ToString()
    {
        return $"{Name} (Rank {Rank}) (ID: {ID})";
    }
}

public class Judgement : Spell { }

public class Talent : Spell 
{ 
    public static List<Talent> GetTalents(params int[] talents)
    {
        List<Talent> results = new();

        foreach (int talent in talents)
        {
            if (Data.Collections.Talents[talent] is Talent t)
                results.Add(t);

            else
                throw new Exception($"The given talent with ID {talent} was not a valid talent.");
        }

        return results;
    }
}

public enum SpellTarget
{
    Self = 0,
    Enemy = 1,
    Ally = 2
}

public class SpellGCD
{
    public int Duration { get; init; } = Constants.Numbers.DefaultGCD;

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