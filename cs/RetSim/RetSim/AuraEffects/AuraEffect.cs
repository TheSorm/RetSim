using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public abstract class AuraEffect
    {
        public AuraEffect() { }

        public abstract void Apply(Player caster, Aura aura, int time, List<Event> resultingEvents);
        public abstract void Remove(Player caster, Aura aura, int time, List<Event> resultingEvents);
    }
}