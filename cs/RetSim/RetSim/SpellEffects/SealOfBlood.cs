using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.SpellEffects
{
    public class SealOfBlood : SpellEffect
    {

        public SealOfBlood() : base()
        {
        }

        public override ProcMask Resolve(Player caster, Spell spell, int time, List<Event> resultingEvents)
        {
            int damage = Formulas.Damage.NormalizedWeaponDamage(341, 513, 0, 2000, 0.35f);

            return ProcMask.None; // TODO: Is that all?
        }
    }
}
