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

public class SchoolModifiers : FailsafeDictionary<School, float>
{
    public SchoolModifiers()
    {
        Default = 1f;

        foreach (School school in Enum.GetValues(typeof(School)))
        {
            Add(school, Default);
        }
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