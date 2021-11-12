using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.Enemy;
using RetSim.Units.UnitStats;

namespace RetSim.Spells.AuraEffects;

class GainStatsCreature : GainStats
{
    public List<CreatureType> Creatures { get; init; }

    protected bool MatchingType = false;

    public GainStatsCreature(StatSet stats, List<CreatureType> creatures) : base(stats)
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