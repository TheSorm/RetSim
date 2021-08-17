using RetSim.Events;
using System;
using System.Collections.Generic;

namespace RetSim
{
    public partial class Player
    {
        private AutoAttackEvent nextAutoAttack;
        private Dictionary<int, Event> spellIdToCooldownEndEvent = new();
        private Dictionary<int, Func<int, List<Event>, int>> spellIdToSpellCast = new();

        public Player()
        {
            foreach (var spellEntry in Spellbook.ByID)
            {
                spellIdToCooldownEndEvent.Add(spellEntry.Key, null);
            }

            spellIdToSpellCast.Add(Spellbook.crusaderStrike.ID, (time, resultingEvents) => CastCrusaderStrike(time, resultingEvents));
        }

        public int CastSpell(int spellId, int time, List<Event> resultingEvents)
        {
            int cooldown = Spellbook.ByID[spellId].Cooldown;
            if (cooldown > 0)
            {
                Event cooldownEnd = new CooldownEndEvent(time + cooldown, this, spellId);
                resultingEvents.Add(cooldownEnd);
                spellIdToCooldownEndEvent[spellId] = cooldownEnd;
            }
            return spellIdToSpellCast[spellId](time, resultingEvents);
        }

        public int CastCrusaderStrike(int time, List<Event> resultingEvents)
        {
            return 1212;
        }

        public int TimeOfNextSwing()
        {
            return nextAutoAttack.ExpirationTime;
        }

        public int MeleeAttack(int time, List<Event> resultingEvents)
        {
            nextAutoAttack = new AutoAttackEvent(time + 3500, this);
            resultingEvents.Add(nextAutoAttack);
            return 1234;
        }

        public void RemoveCooldownOf(int id)
        {
            spellIdToCooldownEndEvent[id] = null;
        }

        public bool IsSpellOnCooldown(int id)
        {
            return spellIdToCooldownEndEvent[id] != null;
        }

        public int GetEndOfCooldown(int id)
        {
            return spellIdToCooldownEndEvent[id] != null ? spellIdToCooldownEndEvent[id].ExpirationTime : 0;
        }
    }
}

