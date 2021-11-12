using RetSim.Spells;
using RetSim.Units.UnitStats;
using System.Text.Json.Serialization;

namespace RetSim.Units.Player.Static;

public class Race
{
    public string Name { get; init; }
    public StatSet Stats { get; init; }

    [JsonIgnore]
    public Spell Racial { get; set; }
    public int RacialID { get; init; }

    public override string ToString()
    {
        return Name;
    }
}

public enum Races
{
    Human = 1,
    Dwarf = 2,
    Draenei = 3,
    BloodElf = 4
}