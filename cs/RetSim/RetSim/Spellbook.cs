using RetSim.Events;
using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public class Spellbook : Dictionary<Spell, CooldownEndEvent>
    {
        private readonly Player player;

        public Spellbook(Player caster)
        {
            player = caster;

            foreach (var spell in Glossaries.Spells.ByID.Values)
                Add(spell);
        }

        public new void Add(Spell spell, CooldownEndEvent end = null)
        {
            base.Add(spell, null);
        }

        public void StartCooldown(Spell spell, CooldownEndEvent cooldown)
        {
            this[spell] = cooldown;
        }

        public void EndCooldown(Spell spell)
        {
            this[spell] = null;
        }

        public bool IsOnCooldown(Spell spell)
        {
            return this[spell] != null;
        }

        public bool SufficientMana(Spell spell)
        {
            return spell.ManaCost <= player.Stats.Mana;
        }

        public static ProcMask Use(Spell spell, FightSimulation fight)
        {
            ProcMask mask = ProcMask.None;

            if (spell.Cooldown > 0)
                fight.Queue.Add(new CooldownEndEvent(spell, fight, fight.Timestamp + spell.Cooldown));

            if (spell.GCD != null)
                fight.Queue.Add(new GCDEndEvent(fight, fight.Timestamp + spell.GCD.Duration));

            if (spell.Aura != null)
                fight.Player.Auras.Apply(spell.Aura, fight);

            if (spell.Effects != null)
            {
                foreach (SpellEffect effect in spell.Effects)
                    mask |= effect.Resolve(fight);
            }

            return mask;
        }
    }
}