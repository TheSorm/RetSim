using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.SpellEffects
{
    public class GainAura : SpellEffect
    {
        public Aura Aura { get; init; }
        public bool IsSpellcast { get; set; }

        public GainAura(Aura aura, bool spellcast) : base()
        {
            Aura = aura;
            IsSpellcast = spellcast;
        }

        public override ProcMask Resolve(Player caster, Spell spell, int time, List<Event> results)
        {
            caster.Auras.Apply(Aura, time, results);

            Program.Logger.Log($"Player gains {Aura.Name}.");

            if (IsSpellcast)
                return ProcMask.OnSpellCastSuccess; // TODO: Is that all?

            else
                return ProcMask.None;
        }
    }
}
