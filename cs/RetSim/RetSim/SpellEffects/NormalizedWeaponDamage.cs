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

        public override ProcMask Resolve(Player caster, Spell spell, int time, List<Event> resultingEvents)
        {
            int damage = Formulas.Damage.NormalizedWeaponDamage(341, 513, 0, 2000, Modifier);

            return ProcMask.OnMeleeAutoAttack; // TODO: Is that all?
        }
    }
}
