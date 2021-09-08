using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    class ModDamageSpell : Modify
    {
        public List<Spell> Spells { get; init; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            base.Apply(aura, fight);

            foreach (Spell spell in Spells)
            {
                fight.Player.Modifiers.Spells[spell] *= RelativeDifference;
            }
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            foreach (Spell spell in Spells)
            {
                fight.Player.Modifiers.Spells[spell] /= RelativeDifference;
            }

            base.Remove(aura, fight);
        }
    }
}