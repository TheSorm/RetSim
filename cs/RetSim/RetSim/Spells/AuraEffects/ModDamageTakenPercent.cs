using RetSim.Simulation;
using RetSim.Units;

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
        base.Apply(aura, caster, target, fight);

        target.Modifiers.DamageTaken[SchoolMask] *= Difference;

        //Program.Logger.Log($"{fight.Timestamp} - {caster.Name}'s {aura.Parent.Name} increased {target.Name}'s {SchoolMask} damage taken to {target.Modifiers.SchoolDamageTaken[SchoolMask]}");
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        target.Modifiers.DamageTaken[SchoolMask] /= Difference;

        //Program.Logger.Log($"{fight.Timestamp} - {caster.Name}'s {aura.Parent.Name} decreased {target.Name}'s {SchoolMask} damage taken to {target.Modifiers.SchoolDamageTaken[SchoolMask]}");

        base.Remove(aura, caster, target, fight);
    }
}