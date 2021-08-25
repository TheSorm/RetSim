using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    public class GainProc : AuraEffect
    {
        public Proc Proc { get; init; }

        public GainProc(Proc proc) : base()
        {
            Proc = proc;
        }

        public override void Apply(Player caster, Aura aura, int time, List<Event> resultingEvents)
        {
            caster.Procs.Add(Proc);
        }
        public override void Remove(Player caster, Aura aura, int time, List<Event> resultingEvents)
        {
            caster.Procs.Remove(Proc);
        }
    }
}
