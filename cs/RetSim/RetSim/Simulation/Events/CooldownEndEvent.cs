using RetSim.Spells;
using RetSim.Units.Player.State;

namespace RetSim.Simulation.Events;

public class CooldownEndEvent : Event
{
    private const int BasePriority = 2;

    private SpellState State { get; init; }

    public CooldownEndEvent(SpellState state, FightSimulation fight, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
    {
        State = state;

        Spellbook.StartCooldown(state, this);
    }

    public override ProcMask Execute(object arguments = null)
    {
        Spellbook.EndCooldown(State);

        return ProcMask.None;
    }

    public override string ToString()
    {
        return $"Cooldown of {State.Spell.Name} ends";
    }
}