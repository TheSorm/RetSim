using RetSim.Events;
using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public class PlayerSpell
    {
        public Spell Spell { get; private set; }
        public CooldownEndEvent Cooldown { get; private set; }

        public PlayerSpell(Spell spell)
        {
            Spell = spell;
            Cooldown = null;
        }

        public bool IsOnCooldown
        {
            get { return Cooldown == null; }
        }

        public bool SufficientMana(int mana)
        {
            return mana >= Spell.ManaCost;
        }

        public List<Event> Use(int start, Player caster)
        {
            //TODO: Fix mana check & remove mana from player

            var events = new List<Event>();

            if (IsOnCooldown)
            {
                //Add spell cast fail - cooldown not ready event
            }

            else if (SufficientMana(500))
            {
                //Add spell cast fail - insufficient mana event
            }

            else
            {
                foreach (SpellEffect effect in Spell.Effects)
                {
                    effect.Resolve(caster);
                }

                events.Add(StartCooldown(start, caster));
                events.Add(new GCDEndEvent(start + Spell.GCD.Duration, caster));
            }

            return events;
        }

        public CooldownEndEvent StartCooldown(int start, Player player)
        {
            if (Spell.Cooldown > 0 && Cooldown == null)
            {
                Cooldown = new CooldownEndEvent(start + Spell.Cooldown, player, Spell.ID);

                return Cooldown;
            }

            else return null;
        }

        public bool EndCooldown()
        {
            if (Cooldown != null)
            {
                Cooldown = null;

                return true;
            }

            else return false;
        }
    }
}
