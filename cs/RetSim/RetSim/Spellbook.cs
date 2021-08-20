using RetSim.Events;
using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public class Spellbook : Dictionary<Spell, CooldownEndEvent>
    {
        private Player player { get; init; }

        //TODO Remove
        public Dictionary<int, Spell> ByID = new Dictionary<int, Spell>();

        public Spellbook(Player player)
        {
            this.player = player;

            Add(Spells.CrusaderStrike, null);
            Add(Spells.SealOfCommand, null);
            Add(Spells.SealOfBlood, null);
            Add(Spells.SealOfTheCrusader, null);
        }

        //TODO remove check
        public new void Add(Spell spell, CooldownEndEvent end = null)
        {
            if (ContainsKey(spell))
                return;

            else
            {
                base.Add(spell, null);
                ByID.Add(spell.ID, spell);
            }      
        }

        public bool IsOnCooldown(Spell spell)
        {
            return this[spell] != null;
        }

        public bool SufficientMana(Spell spell)
        {
            return spell.ManaCost <= player.Mana;
        }

        //TODO remove checks
        public List<Event> Use(Spell spell, int time)
        {
            //TODO: Fix mana check & remove mana from player

            var events = new List<Event>();

            if (IsOnCooldown(spell))
            {
                //Add spell cast fail - cooldown not ready event
            }

            else if (player.IsOnGCD())
            {
                //Add spell cast fail - cannot use ability yet
            }

            else if (!SufficientMana(spell))
            {
                //Add spell cast fail - insufficient mana event
            }

            else
            {
                foreach (SpellEffect effect in spell.Effects)
                {
                    events.AddRange(effect.Resolve(player, spell, time));
                }

                if (spell.Cooldown > 0)
                    events.Add(StartCooldown(spell, time));

                if (spell.GCD.Category != GCDCategory.None && spell.GCD.Duration > 0)
                {
                    var gcd = new GCDEndEvent(time + spell.GCD.Duration, player);

                    player.StartGCD(gcd);
                    events.Add(gcd);
                }
            }

            return events;
        }

        private CooldownEndEvent StartCooldown(Spell spell, int time)
        {
            if (spell.Cooldown > 0 && !IsOnCooldown(spell))
            {
                var cooldown = new CooldownEndEvent(time + spell.Cooldown, player, spell.ID);

                this[spell] = cooldown;

                return cooldown;
            }

            else return null;
        }

        //TODO remove check
        public bool EndCooldown(Spell spell)
        {
            if (IsOnCooldown(spell))
            {
                this[spell] = null;

                return true;
            }

            else return false;
        }
    }
}
