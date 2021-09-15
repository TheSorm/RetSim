using RetSim.Spells;

namespace RetSim.Units.Player.State;

public class Modifiers
{
    public SchoolModifiers SchoolModifiers { get; init; } = new SchoolModifiers();
    public SchoolBonuses SchoolBonuses { get; init; } = new SchoolBonuses();

    public float AttackSpeed { get; set; } = 1f;
    public float CastSpeed { get; set; } = 1f;
    public float WeaponDamage { get; set; } = 1f;
}

public abstract class FailsafeDictionary<Key, Value> : Dictionary<Key, Value>
{
    protected Value Default { get; init; }

    public Value GetValue(Key key)
    {
        if (ContainsKey(key))
            return this[key];

        else
            return Default;
    }
}

public class SchoolModifiers
{
    protected readonly static float Default = 1f;
    protected Dictionary<School, float> map;

    public SchoolModifiers()
    {
        map = new();
    }

    public float this[School school]
    {
        get
        {
            if (map.ContainsKey(school))
                return map[school];

            else
                return Default;
        }

        set 
        {
            if (map.ContainsKey(school))
            {
                if (value == Default)
                    map.Remove(school);

                else
                    map[school] = value;
            }

            else
                map.Add(school, value);
        }    
    }

    public float GetModifier(School school)
    {
        float mod = Default;

        foreach ((School key, float value) in map)
        {
            if ((key & school) != 0)
                mod *= value;
        }

        return mod;
    }
}

public class SchoolBonuses : FailsafeDictionary<School, int>
{
    public SchoolBonuses()
    {
        Default = 1;

        foreach (School school in Enum.GetValues(typeof(School)))
        {
            Add(school, Default);
        }
    }
}