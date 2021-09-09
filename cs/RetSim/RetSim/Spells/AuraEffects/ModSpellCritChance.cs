using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    class ModSpellCritChance : ModifyFlat
    {
        public List<int> Spells { get; init; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            base.Apply(aura, fight);

            foreach (int spell in Spells)
            {
                fight.Player.Spellbook[spell].BonusCritChance += Difference;
            }
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            foreach (int spell in Spells)
            {
                fight.Player.Spellbook[spell].BonusCritChance -= Difference;
            }

            base.Remove(aura, fight);
        }
    }
}