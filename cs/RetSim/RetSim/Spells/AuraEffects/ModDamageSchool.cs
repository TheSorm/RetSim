using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

class ModDamageSchool : ModifyPercent
{
    public ModDamageSchool(int percent, School schoolMask) : base(percent)
    {
        SchoolMask = schoolMask;
    }

    public School SchoolMask { get; init; }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        base.Apply(aura, caster, target, fight);

        fight.Player.Modifiers.SchoolDamageDone[SchoolMask] *= RelativeDifference;

        //Program.Logger.Log($"{fight.Timestamp} - {aura.Parent.Name} increased {SchoolMask} damage modifier to {fight.Player.Modifiers.SchoolModifiers[SchoolMask]}");
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        fight.Player.Modifiers.SchoolDamageDone[SchoolMask] /= RelativeDifference;

        //Program.Logger.Log($"{fight.Timestamp} - {aura.Parent.Name} decreased {SchoolMask} damage modifier to {fight.Player.Modifiers.SchoolModifiers[SchoolMask]}");

        base.Remove(aura, caster, target, fight);
    }
}