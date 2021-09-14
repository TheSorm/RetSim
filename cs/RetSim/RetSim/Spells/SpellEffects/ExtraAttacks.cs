using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSim.Simulation.Events;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;

namespace RetSim.Spells.SpellEffects;

class ExtraAttacks : SpellEffect
{
    Spell Proc { get; init; }
    int Number { get; init; }

    public ExtraAttacks(Spell proc, int number)
    {
        Proc = proc;
        Number = number;
    }

    public override ProcMask Resolve(FightSimulation fight, SpellState state)
    {
        var entry = new ExtraAttacksEntry
        {
            Timestamp = fight.Timestamp,
            Mana = (int)fight.Player.Stats[StatName.Mana].Value,
            Source = Parent.Name,
            Number = Number
        };

        fight.CombatLog.Add(entry);

        for (int i = 0; i < Number; i++)
        {
            fight.Queue.Add(new CastEvent(Proc, fight.Player, fight.Enemy, fight, fight.Timestamp));
        }

        return ProcMask.None;
    }
}