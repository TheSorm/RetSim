using System;
using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    class ModDamage : AuraEffect
    {
        public List<School> Schools { get; init; }
        public int Percentage { get; init; }

        private int CurrentMod = 100;
        private float PreviousMod;
        private float RelativeDifference;

        public override void Apply(Aura aura, FightSimulation fight)
        {
            PreviousMod = CurrentMod;
            CurrentMod += Percentage;            
            RelativeDifference = CurrentMod / PreviousMod;

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

            CurrentMod = (int)PreviousMod;
            PreviousMod -= Percentage;
            RelativeDifference = CurrentMod / PreviousMod;
        }
    }
}
