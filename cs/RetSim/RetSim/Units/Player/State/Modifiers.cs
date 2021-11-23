using RetSim.Spells;

namespace RetSim.Units.Player.State;

public class Modifiers
{
    public ModifiersPercent DamageDone { get; init; } = new ModifiersPercent();
    public ModifiersPercent DamageTaken { get; init; } = new ModifiersPercent();
    public ModifiersFlat DamageTakenFlat { get; init; } = new ModifiersFlat();

    public float AttackSpeed { get; set; } = 1f;
    public float CastSpeed { get; set; } = 1f;
    public float WeaponDamage { get; set; } = 1f;
}

public class ModifiersPercent
{
    protected readonly static float Default = 1f;
    protected Dictionary<School, float> map; //TODO: Turn into array?

    public ModifiersPercent()
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

public class ModifiersFlat
{
    protected int[] Values;

    public ModifiersFlat()
    {
        Values = new int[(int)School.All + 1];

        foreach (int school in Values)
            Values[school] = 0;
    }

    public int this[School school]
    {
        get => Values[(int)school];

        set => Values[(int)school] = value;
    }
}