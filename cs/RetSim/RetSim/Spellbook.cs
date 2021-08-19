using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class Spellbook : List<PlayerSpell>
    {
        public Dictionary<int, PlayerSpell> ByID = new Dictionary<int, PlayerSpell>();

        private Player player { get; init; }

        public Spellbook(Player player)
        {
            this.player = player;

            Add(new PlayerSpell(Spells.CrusaderStrike));
        }

        public new void Add(PlayerSpell spell)
        {
            if (Contains(spell))
                return;

            else
            {
                base.Add(spell);
                ByID.Add(spell.Spell.ID, spell);
            }
        }

        public List<Event> Use(PlayerSpell spell, int time)
        {
            List<Event> events = spell.Use(time, player);

            return events;
        }


    }
}
