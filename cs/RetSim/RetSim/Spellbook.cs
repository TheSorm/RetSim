using RetSim.Events;
using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public class Spellbook : Dictionary<Spell, CooldownEndEvent>
    {
        private Player player { get; init; }

        public Spellbook(Player caster)
        {
            player = caster;

            foreach (var spell in Glossaries.Spells.ByID.Values)
            {
                Add(spell);
            }
        }

        public new void Add(Spell spell, CooldownEndEvent end = null)
        {
            base.Add(spell, null);
        }

        public bool IsOnCooldown(Spell spell)
        {
            return this[spell] != null;
        }

        public bool SufficientMana(Spell spell)
        {
            return spell.ManaCost <= player.Stats.Mana;
        }

        public ProcMask Use(Spell spell, int time, List<Event> resultingEvents)
        {
            ProcMask procMask = ProcMask.None;
            foreach (SpellEffect effect in spell.Effects)
            {
                procMask |= effect.Resolve(player, spell, time, resultingEvents);
            }

            if (spell.Cooldown > 0)
                resultingEvents.Add(StartCooldown(spell, time));

            if (spell.GCD.Category != GCDCategory.None && spell.GCD.Duration > 0)
            {
                var gcd = new GCDEndEvent(time + spell.GCD.Duration, player);

                player.StartGCD(gcd);
                resultingEvents.Add(gcd);
            }

            return procMask;
        }

        private CooldownEndEvent StartCooldown(Spell spell, int time)
        {
            if (spell.Cooldown > 0 && !IsOnCooldown(spell))
            {
                var cooldown = new CooldownEndEvent(time + spell.Cooldown, player, spell);

                this[spell] = cooldown;

                return cooldown;
            }

            else return null;
        }

        public void EndCooldown(Spell spell)
        {
            this[spell] = null;
        }
    }
}
