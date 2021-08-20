using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.SpellEffects
{
    public abstract class SpellEffect
    {
        public SpellEffect() { }

        public abstract void Resolve(Player caster, Spell spell, int time, List<Event> resultingEvents);
    }
}