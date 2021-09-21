using RetSim.Simulation;
using RetSim.Units.Player.State;
using System.Text.Json.Serialization;

namespace RetSim.Spells.SpellEffects;

[Serializable]
[JsonSerializable(typeof(object))]
public abstract class SpellEffect
{
    [JsonIgnore]
    public Spell Parent { get; set; }
    public float MinEffect { get; init; }
    public float MaxEffect { get; init; }

    public abstract ProcMask Resolve(FightSimulation fight, SpellState state);

    public override string ToString()
    {
        return $"{GetType()} effect owned by {Parent.Name}";
    }
}