using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.SpellEffects
{
    public class NormalizedWeaponDamage : SpellEffect
    {
        public float Modifier { get; init; }

        public NormalizedWeaponDamage(float modifier) : base()
        {
            Modifier = modifier;
        }

        //TODO dont return event
        public override List<Event> Resolve(Player caster, Spell spell, int time)
        {
            int damage = Formulas.Damage.NormalizedWeaponDamage(341, 513, 0, 2000, Modifier);

            return new List<Event> { new DamageEvent(time, caster, spell.Name, "Hit", damage, spell.School) };
        }
    }
}
