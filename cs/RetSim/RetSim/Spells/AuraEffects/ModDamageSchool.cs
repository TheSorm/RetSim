using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    class ModDamageSchool : ModifyPercent
    {
        public List<School> Schools { get; init; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            base.Apply(aura, fight);

            foreach (School school in Schools)
            {
                fight.Player.Modifiers.Schools[school] *= RelativeDifference;
            }
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            foreach (School school in Schools)
            {
                fight.Player.Modifiers.Schools[school] /= RelativeDifference;
            }

            base.Remove(aura, fight);
        }
    }
}