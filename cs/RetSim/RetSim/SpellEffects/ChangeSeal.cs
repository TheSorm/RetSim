using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.SpellEffects
{
    public class ChangeSeal : GainAura
    {
        public Seal Seal { get; init; }

        public ChangeSeal(Seal seal) : base(seal, true)
        {
            Seal = seal;
        }

        public override ProcMask Resolve(Player caster, Spell spell, int time, List<Event> results)
        {
            foreach (Seal other in Seal.ExclusiveWith)
            {
                if (caster.Auras.IsActive(other))
                {
                    if (other.Persist == 0)
                        caster.Auras[other].End.ExpirationTime = time;
                    else
                        caster.Auras[other].End.ExpirationTime = time + other.Persist;
                }
            }

            return base.Resolve(caster, spell, time, results);
        }
    }
}
