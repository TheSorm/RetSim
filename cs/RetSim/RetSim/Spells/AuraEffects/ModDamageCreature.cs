using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.Enemy;

namespace RetSim.Spells.AuraEffects;

class ModDamageCreature : ModDamageSchool
{
    public List<CreatureType> Types { get; init; }

    protected bool MatchingType = false;
    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        if (Types.Contains(fight.Enemy.Type))
        {
            MatchingType = true;
            base.Apply(aura, caster, target, fight);
        }
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        if (MatchingType)
            base.Remove(aura, caster, target, fight);
    }
}