using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

class ModCastSpeed : ModifyPercent
{
    public ModCastSpeed(float percent) : base(percent) { }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        fight.Player.Modifiers.CastSpeed *= GetDifference(Value, target.Auras[aura].Stacks);

        //Program.Logger.Log($"{fight.Timestamp} - {target.Name} gains {Value}% bonus cast speed from {aura.Parent.Name}. Current: {fight.Player.Modifiers.CastSpeed} / Effective: {fight.Player.Stats.EffectiveCastSpeed}");
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        fight.Player.Modifiers.CastSpeed /= GetDifference(Value, target.Auras[aura].Stacks);

        //Program.Logger.Log($"{fight.Timestamp} - {target.Name} loses {Value}% bonus cast speed from {aura.Parent.Name}. Current: {fight.Player.Modifiers.CastSpeed} / Effective: {fight.Player.Stats.EffectiveCastSpeed}");
    }
}