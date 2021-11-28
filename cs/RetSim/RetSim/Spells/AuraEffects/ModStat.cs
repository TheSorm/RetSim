using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.Player.State;
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
            SpellState state = fight.Player.Spellbook[aura.Parent.ID];

            int index = aura.Effects.IndexOf(this);

            EffectBonus bonuses = state.AuraBonuses[index];

            target.Stats[stat].Bonus += (Value + bonuses.Flat) * bonuses.Percent;

            //Program.Logger.Log($"{fight.Timestamp} - {target.Name} gains {(Value + bonuses.Flat) * bonuses.Percent} {stat} from {aura.Parent.Name}. Current: {target.Stats[stat].Value}");
        }
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        foreach (StatName stat in Stats)
        {
            SpellState state = fight.Player.Spellbook[aura.Parent.ID];

            int index = aura.Effects.IndexOf(this);

            EffectBonus bonuses = state.AuraBonuses[index];

            target.Stats[stat].Bonus -= (Value + bonuses.Flat) * bonuses.Percent;

            //Program.Logger.Log($"{fight.Timestamp} - {target.Name} loses {(Value + bonuses.Flat) * bonuses.Percent} {stat} from {aura.Parent.Name}. Current: {target.Stats[stat].Value}");
        }
    }
}