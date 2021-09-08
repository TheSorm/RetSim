using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    class ModSpellCritChance : Modify
    {
        public List<Spell> Spells { get; init; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            base.Apply(aura, fight);

            foreach (Spell spell in Spells)
            {
                fight.Player.Modifiers.SpellCrit[spell] *= RelativeDifference;
            }
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            foreach (Spell spell in Spells)
            {
                fight.Player.Modifiers.SpellCrit[spell] /= RelativeDifference;
            }

            base.Remove(aura, fight);
        }
    }
}