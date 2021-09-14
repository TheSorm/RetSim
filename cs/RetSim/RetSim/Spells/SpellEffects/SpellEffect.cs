using RetSim.Simulation;
using RetSim.Units.Player.State;

namespace RetSim.Spells.SpellEffects;

public abstract class SpellEffect
{
    public Spell Parent { get; set; }
    public float MinEffect { get; init; }
    public float MaxEffect { get; init; }

    public abstract ProcMask Resolve(FightSimulation fight, SpellState state);
}