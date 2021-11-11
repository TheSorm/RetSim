using RetSim.Simulation;
using RetSim.Units.Player.State;
using System.Text.Json.Serialization;

namespace RetSim.Spells.SpellEffects;

public abstract class SpellEffect
{
    [JsonIgnore]
    public Spell Parent { get; set; }
    public float MinEffect { get; init; }
    public float MaxEffect { get; init; }

    public SpellEffect(float min, float max)
    {
        MinEffect = min;
        MaxEffect = max;
    }

    public abstract ProcMask Resolve(FightSimulation fight, SpellState state);

    public override string ToString()
    {
        return $"{GetType()} effect owned by {Parent.Name}";
    }
}