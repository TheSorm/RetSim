using RetSim.Simulation.CombatLogEntries;
using RetSim.Spells;
using RetSim.Spells.SpellEffects;
using RetSim.Units;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;

namespace RetSim.Simulation.Events;

public class DoTEvent : Event
{
    private const int BasePriority = 3;

    private SpellEffect Spell { get; init; }
    private SpellState State { get; init; }
    private Unit Caster { get; init; }
    private Unit Target { get; init; }

    private int RemainingTicks { get; init; }
    private int Interval { get; init; }

    public DoTEvent(SpellEffect spell, SpellState state, Unit caster, Unit target, FightSimulation fight, int remainingTicks, int interval, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
    {
        Spell = spell;
        State = state;
        Caster = caster;
        Target = target;

        RemainingTicks = remainingTicks;
        Interval = interval;
    }

    public override ProcMask Execute(object arguments = null)
    {
        ProcMask result = Spell.Resolve(Fight, State);

        if (RemainingTicks > 1)
        {
            if (Fight.Timestamp + Interval <= Fight.Duration)
                Fight.Queue.Add(new DoTEvent(Spell, State, Caster, Target, Fight, RemainingTicks - 1, Interval, Fight.Timestamp + Interval));
        }

        else
        {
            var entry = new DebuffEntry()
            {
                Timestamp = Fight.Timestamp,
                Mana = (int)Fight.Player.Stats[StatName.Mana].Value,
                Source = Spell.Parent.Name,
                Stacks = 0,
                Type = AuraChangeType.Fade
            };

            Fight.CombatLog.Add(entry);
        }

        return result;
    }

    public override string ToString()
    {
        return $"{Caster.Name}'s {Spell.Parent.Name} deals periodic damage to {Target.Name} - {RemainingTicks} ticks left / {Interval} DoT interval";
    }
}