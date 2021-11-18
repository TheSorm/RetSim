using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSim.Simulation.Events;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;

using Newtonsoft.Json;

namespace RetSim.Spells.SpellEffects;

public class ExtraAttacks : SpellEffect
{
    [JsonIgnore]
    private Spell Proc { get; set; }
    public int ProcID { get; init; }

    public ExtraAttacks(int value, int procID) : base(value, 0)
    {
        ProcID = procID;
    }

    public override ProcMask Resolve(FightSimulation fight, SpellState state)
    {
        if (Proc == null)
            Proc = Data.Collections.Spells[ProcID];

        var entry = new ExtraAttacksEntry
        {
            Timestamp = fight.Timestamp,
            Mana = (int)fight.Player.Stats[StatName.Mana].Value,
            Source = Parent.Name,
            Number = (int)Value
        };

        fight.CombatLog.Add(entry);

        for (int i = 0; i < Value; i++)
        {
            fight.Queue.Add(new CastEvent(Proc, fight.Player, fight.Enemy, fight, fight.Timestamp));
        }

        return ProcMask.None;
    }
}