using RetSim.Data;
using RetSim.Simulation;
using RetSim.Simulation.Events;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

public class CancelAuraOnRemove : AuraEffect
{
    public List<int> Spells { get; init; }

    public CancelAuraOnRemove(List<int> spells) : base(0)
    {
        Spells = spells;
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (int spell in Spells)
        {
            if (Collections.Auras.ContainsKey(spell))
            {
                var spellAura = Collections.Auras[spell];

                if (fight.Player.Auras.IsActive(spellAura))
                {
                    var end = fight.Player.Auras[spellAura].End;

                    if (end != null)
                        end.Timestamp = fight.Timestamp + 1;

                    else
                        fight.Queue.Add(new AuraEndEvent(spellAura, fight.Player, fight.Enemy, fight, fight.Timestamp + 1));
                }

                else
                    throw new Exception($"The given aura (ID: {spell}, Name: {spellAura.Parent.Name}) was not active.");
            }

            else
                throw new Exception($"The given aura (ID: {spell}) does not exist.");
        }
    }
}