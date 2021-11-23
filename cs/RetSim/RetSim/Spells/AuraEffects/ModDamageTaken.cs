using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.Player.State;

namespace RetSim.Spells.AuraEffects;

class ModDamageTaken : AuraEffect
{
    public ModDamageTaken(float amount, School school) : base(amount)
    {
        School = school;
    }

    public School School { get; init; }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        SpellState state = fight.Player.Spellbook[aura.Parent.ID];

        int index = aura.Effects.IndexOf(this);

        EffectBonus bonuses = state.AuraBonuses[index];

        float amount = (Value + bonuses.Flat) * bonuses.Percent;

        target.Modifiers.DamageTakenFlat[School] += (int)amount;

        //Program.Logger.Log($"{fight.Timestamp} - {caster.Name}'s {aura.Parent.Name} increased {target.Name}'s {School} damage taken by {amount}. Current: {target.Modifiers.DamageTakenFlat[School]}");
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        SpellState state = fight.Player.Spellbook[aura.Parent.ID];

        int index = aura.Effects.IndexOf(this);

        EffectBonus bonuses = state.AuraBonuses[index];

        float amount = (Value + bonuses.Flat) * bonuses.Percent;

        target.Modifiers.DamageTakenFlat[School] -= (int)amount;

        //Program.Logger.Log($"{fight.Timestamp} - {caster.Name}'s {aura.Parent.Name} decreased {target.Name}'s {School} damage taken by {amount}. Current: {target.Modifiers.DamageTakenFlat[School]}");
    }
}