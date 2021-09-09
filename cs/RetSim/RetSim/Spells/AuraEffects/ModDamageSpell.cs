using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    class ModDamageSpell : ModifyPercent
    {
        public List<int> Spells { get; init; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            base.Apply(aura, fight);

            foreach (int spell in Spells)
            {
                fight.Player.Spellbook[spell].EffectBonusPercent *= RelativeDifference;
            }
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            foreach (int spell in Spells)
            {
                fight.Player.Spellbook[spell].EffectBonusPercent /= RelativeDifference;
            }

            base.Remove(aura, fight);
        }
    }
}