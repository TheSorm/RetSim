using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.UnitStats;

namespace RetSim.Spells.AuraEffects;

class ModStatPercent : ModifyPercent
{
    public ModStatPercent(float percent, List<StatName> stats) : base(percent)
    {
        Stats = stats;
    }

    public List<StatName> Stats { get; init; }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        base.Apply(aura, caster, target, fight);

        foreach (StatName stat in Stats)
        {
            target.Stats[stat].Modifier *= Difference;
        }
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (StatName stat in Stats)
        {
            target.Stats[stat].Modifier /= Difference;
        }

        base.Remove(aura, caster, target, fight);
    }
}