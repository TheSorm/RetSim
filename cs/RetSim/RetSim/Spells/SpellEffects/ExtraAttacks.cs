using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSim.Simulation.Events;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;

namespace RetSim.Spells.SpellEffects;

public class ExtraAttacks : SpellEffect
{
    public int ProcID { get; init; }
    public Spell Proc { get; init; }
    public int Amount { get; init; }

    public ExtraAttacks()
    {

    }

    public ExtraAttacks(Spell proc, int amount)
    {
        Proc = proc;
        Amount = amount;
    }

    public override ProcMask Resolve(FightSimulation fight, SpellState state)
    {
        var entry = new ExtraAttacksEntry
        {
            Timestamp = fight.Timestamp,
            Mana = (int)fight.Player.Stats[StatName.Mana].Value,
            Source = Parent.Name,
            Number = Amount
        };

        fight.CombatLog.Add(entry);

        for (int i = 0; i < Amount; i++)
        {
            fight.Queue.Add(new CastEvent(Proc, fight.Player, fight.Enemy, fight, fight.Timestamp));
        }

        return ProcMask.None;
    }
}