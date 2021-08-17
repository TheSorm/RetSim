using System;
using System.Collections.Generic;

namespace RetSim
{
    public partial class Player
    {
        private int timeOfLastSwing;
        private Dictionary<int, int> spellIdToLastCastTime = new();
        private Dictionary<int, Func<int, List<Event>, int>> spellIdToSpellCast = new();

        public Player()
        {
            foreach (var spellEntry in Spellbook.ByID)
            {
                spellIdToLastCastTime.Add(spellEntry.Key, int.MinValue);
            }

            spellIdToSpellCast.Add(Spellbook.crusaderStrike.ID, (time, resultingEvents) => CastCrusaderStrike(time, resultingEvents));
        }

        public int CastSpell(int spellId, int time, List<Event> resultingEvents)
        {
            int cooldown = Spellbook.ByID[spellId].Cooldown;
            if (cooldown > 0)
            {
                resultingEvents.Add(new CooldownEndEvent(time + cooldown, this, spellId));
            }
            return spellIdToSpellCast[spellId](time, resultingEvents);
        }

        public int CastCrusaderStrike(int time, List<Event> resultingEvents)
        {
            spellIdToLastCastTime[Spellbook.crusaderStrike.ID] = time;
            return 1212;
        }

        public int TimeOfNextSwing()
        {
            return timeOfLastSwing + 3500;
        }

        public int MeleeAttack(int time)
        {
            timeOfLastSwing = time;
            return 1234;
        }

        public bool IsSpellOnCooldown(int id, int time)
        {
            return spellIdToLastCastTime[id] + Spellbook.ByID[id].Cooldown > time;
        }

        public int GetCooldwonRemaining(int id, int time)
        {
            return spellIdToLastCastTime[id] + Spellbook.ByID[id].Cooldown - time;
        }
    }
}

