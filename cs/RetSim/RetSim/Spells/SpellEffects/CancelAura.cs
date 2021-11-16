using RetSim.Data;
using RetSim.Simulation;
using RetSim.Simulation.Events;
using RetSim.Units.Player.State;

namespace RetSim.Spells.SpellEffects;

class CancelAura : SpellEffect
{
    public CancelAura() : base(0, 0)
    {
    }

    public override ProcMask Resolve(FightSimulation fight, SpellState state)
    {
        if (Collections.Auras.ContainsKey((int)MinEffect))
        {
            var aura = Collections.Auras[(int)MinEffect];

            if (fight.Player.Auras.IsActive(aura))
            {
                var end = fight.Player.Auras[aura].End;

                if (end != null)
                    end.Timestamp = fight.Timestamp + 1;

                else
                    fight.Queue.Add(new AuraEndEvent(aura, fight.Player, fight.Enemy, fight, fight.Timestamp + 1));
            }

            else 
                throw new Exception($"The given aura (ID: {MinEffect}, Name: {aura.Parent.Name}) was not active.");
        }

        else
            throw new Exception($"The given aura (ID: {MinEffect}) does not exist.");
        
        return ProcMask.None;
    }
}