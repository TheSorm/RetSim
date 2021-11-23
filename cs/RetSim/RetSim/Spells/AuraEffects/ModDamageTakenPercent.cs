using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.Player.State;

namespace RetSim.Spells.AuraEffects;

class ModDamageTakenPercent : ModifyPercent
{
    public ModDamageTakenPercent(float percent, School schoolMask) : base(percent)
    {
        SchoolMask = schoolMask;
    }

    public School SchoolMask { get; init; }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        SpellState state = fight.Player.Spellbook[aura.Parent.ID];

        int index = aura.Effects.IndexOf(this);

        EffectBonus bonuses = state.AuraBonuses[index];

        float amount = (Value + bonuses.Flat) * bonuses.Percent;

        target.Modifiers.DamageTaken[SchoolMask] *= GetDifference(amount, target.Auras[aura].Stacks);

        //Program.Logger.Log($"{fight.Timestamp} - {caster.Name}'s {aura.Parent.Name} increased {target.Name}'s {SchoolMask} damage taken by {amount}%. Current: {target.Modifiers.DamageTaken[SchoolMask]}");
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        SpellState state = fight.Player.Spellbook[aura.Parent.ID];

        int index = aura.Effects.IndexOf(this);

        EffectBonus bonuses = state.AuraBonuses[index];

        float amount = (Value + bonuses.Flat) * bonuses.Percent;

        target.Modifiers.DamageTaken[SchoolMask] /= GetDifference(amount, target.Auras[aura].Stacks);

        //Program.Logger.Log($"{fight.Timestamp} - {caster.Name}'s {aura.Parent.Name} decreased {target.Name}'s {SchoolMask} damage taken by {amount}%. Current: {target.Modifiers.DamageTaken[SchoolMask]}");
    }
}