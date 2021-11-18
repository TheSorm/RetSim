using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

public abstract class AuraEffect
{
    public float Value { get; init; }

    public AuraEffect(float value) 
    {
        Value = value;
    }

    public abstract void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight);
    public abstract void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight);
}