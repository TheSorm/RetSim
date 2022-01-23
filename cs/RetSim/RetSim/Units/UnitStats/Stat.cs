namespace RetSim.Units.UnitStats;

public abstract class Stat
{
    public StatName Name { get; init; }
    public float Value { get; set; }

    public float Race { get; init; }
    public float Gear { get; init; }
    public float Permanent { get; set; }

    protected float modifier;
    public float Modifier
    {
        get => modifier;

        set
        {
            modifier = value;
            Update();
        }
    }

    protected float bonus;
    public virtual float Bonus
    {
        get => bonus;

        set
        {
            bonus = value;
            Update();
        }
    }

    protected float rating;
    public float RatingValue
    {
        get => rating;

        set
        {
            rating = value;
            Update();
        }
    }

    public List<SupportedStat> SupportedStats { get; init; }

    protected float support;
    public float SupportValue
    {
        get => support;

        set
        {
            support = value;
            Update();
        }
    }

    public Stat(StatName name, float race, float gear, params (Stat supports, float ratio)[] supportedStats)
    {
        Name = name;
        Modifier = 1f;
        Race = race;
        Gear = gear;
        Permanent = race + gear;

        SupportedStats = new();

        if (supportedStats != null)
        {
            foreach ((Stat Stat, float Ratio) supported in supportedStats)
            {
                SupportedStats.Add(new SupportedStat(this, supported.Stat, supported.Ratio));
            }
        }

        Update();
    }

    public abstract void Update();

    public override string ToString()
    {
        return $"{Name} - Value: {Value} / Base: {Permanent} / Modifier: {Modifier} / Bonus: {Bonus} / Rating: {RatingValue} / Support: {SupportValue}";
    }
}

public class IntegerStat : Stat
{
    public IntegerStat(StatName name, float race, float gear, params (Stat supports, float ratio)[] supportedStats) : base(name, race, gear, supportedStats)
    {
    }

    public override void Update()
    {
        Value = (int)((Permanent + Bonus + RatingValue + SupportValue) * Modifier);

        if (SupportedStats != null)
        {
            foreach (SupportedStat stat in SupportedStats)
                stat.Update();
        }
    }
}

public class DecimalStat : Stat
{
    public DecimalStat(StatName name, float race, float gear, params (Stat supports, float ratio)[] supportedStats) : base(name, race, gear, supportedStats)
    {
    }

    public override void Update()
    {
        Value = (Permanent + Bonus + RatingValue + SupportValue) * Modifier;

        if (SupportedStats != null)
        {
            foreach (SupportedStat stat in SupportedStats)
                stat.Update();
        }
    }
}

public class SupportedStat
{
    private Stat Supporter { get; init; }
    private Stat Supportee { get; init; }
    private float Ratio { get; init; }

    public SupportedStat(Stat supporter, Stat supportee, float ratio)
    {
        Supporter = supporter;
        Supportee = supportee;
        Ratio = ratio;
        Update();
    }

    public void Update()
    {
        Supportee.SupportValue = Supporter.Value / Ratio;
    }
}

public class Rating : IntegerStat
{
    private Stat Supports { get; init; }
    private float Ratio { get; init; }

    public float RatingBonus { get; set; }

    public override float Bonus
    {
        get => bonus;

        set
        {
            bonus = value;
            Update();
            UpdateRating();
        }
    }

    public Rating(StatName name, float race, float gear, Stat supports, float ratio) : base(name, race, gear)
    {
        Supports = supports;
        Ratio = ratio;
        UpdateRating();
    }

    private void UpdateRating()
    {
        RatingBonus = (Permanent + bonus) / Ratio;
        Supports.RatingValue = RatingBonus;
    }
}