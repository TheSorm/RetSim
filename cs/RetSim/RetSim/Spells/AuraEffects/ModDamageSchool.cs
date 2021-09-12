namespace RetSim.AuraEffects
{
    class ModDamageSchool : ModifyPercent
    {
        public List<School> Schools { get; init; }

        public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
        {
            base.Apply(aura, caster, target, fight);

            foreach (School school in Schools)
            {
                fight.Player.Modifiers.SchoolModifiers[school] *= RelativeDifference;
            }
        }

        public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
        {
            foreach (School school in Schools)
            {
                fight.Player.Modifiers.SchoolModifiers[school] /= RelativeDifference;
            }

            base.Remove(aura, caster, target, fight);
        }
    }
}