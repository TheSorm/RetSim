using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.UnitStats;

namespace RetSim.Spells.AuraEffects;

public class ModStat : AuraEffect
{
    public List<StatName> Stats { get; init; }

    public ModStat(float amount, List<StatName> stats) : base(amount)
    {
        Stats = stats;
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (StatName stat in Stats)
        {
            target.Stats[stat].Bonus += Value;
            //Program.Logger.Log($"{fight.Timestamp} - {target.Name}'s {stat.Key} was set to {target.Stats[stat.Key].Value} (previous: {target.Stats[stat.Key].Value - stat.Value})");
        }
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (StatName stat in Stats)
        {
            target.Stats[stat].Bonus -= Value;
            //Program.Logger.Log($"{fight.Timestamp} - {target.Name}'s {stat.Key} was set to {target.Stats[stat.Key].Value} (previous: {target.Stats[stat.Key].Value + stat.Value})");
        }
    }
}