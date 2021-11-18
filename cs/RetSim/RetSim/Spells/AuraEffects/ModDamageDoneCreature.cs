using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.Enemy;

namespace RetSim.Spells.AuraEffects;

class ModDamageDoneCreature : ModDamageDone
{
    public List<CreatureType> Creatures { get; init; }

    protected bool MatchingType = false;

    public ModDamageDoneCreature(float percent, School schoolMask, List<CreatureType> creatures) : base(percent, schoolMask)
    {
        Creatures = creatures;
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        if (Creatures.Contains(fight.Enemy.Type))
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