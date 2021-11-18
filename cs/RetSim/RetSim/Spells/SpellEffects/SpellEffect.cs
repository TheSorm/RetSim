using RetSim.Simulation;
using RetSim.Units.Player.State;
using System.Text.Json.Serialization;

namespace RetSim.Spells.SpellEffects;

public abstract class SpellEffect
{
    [JsonIgnore]
    public Spell Parent { get; set; }

    public float Value { get; init; }
    public float DieSides { get; init; }

    public SpellEffect(float value, float dieSides)
    {
        Value = value;
        DieSides = dieSides;
    }

    public abstract ProcMask Resolve(FightSimulation fight, SpellState state);

    public override string ToString()
    {
        return $"{GetType()} effect owned by {Parent.Name}";
    }
}