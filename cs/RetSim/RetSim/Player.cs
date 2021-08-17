using System;
using System.Collections.Generic;

namespace RetSim
{
    internal class Player
    {
        private int timeOfLastSwing;
        private Dictionary<int, int> spellIdToLastCastTime = new();
        private Dictionary<int, Func<int, List<Event>, int>> spellIdToSpellCast = new();

        internal Player()
        {
            foreach (var spellEntry in Spellbook.byID)
            {
                spellIdToLastCastTime.Add(spellEntry.Key, int.MinValue);
            }

            spellIdToSpellCast.Add(Spellbook.crusaderStrike.SpellId, (time, resultingEvents) => CastCrusaderStrike(time, resultingEvents));
        }

        internal int CastSpell(int spellId, int time, List<Event> resultingEvents)
        {
            int cooldown = Spellbook.byID[spellId].Cooldown;
            if (cooldown > 0)
            {
                resultingEvents.Add(new CooldownEndEvent(time + cooldown, this, spellId));
            }
            return spellIdToSpellCast[spellId](time, resultingEvents);
        }

        internal int CastCrusaderStrike(int time, List<Event> resultingEvents)
        {
            spellIdToLastCastTime[Spellbook.crusaderStrike.SpellId] = time;
            return 1212;
        }

        internal int TimeOfNextSwing()
        {
            return timeOfLastSwing + 3500;
        }

        internal int MeleeAttack(int time)
        {
            timeOfLastSwing = time;
            return 1234;
        }

        internal bool IsSpellOnCooldown(int spellId, int time)
        {
            return spellIdToLastCastTime[spellId] + Spellbook.byID[spellId].Cooldown > time;
        }

        internal int GetCooldwonRemaining(int spellId, int time)
        {
            return spellIdToLastCastTime[spellId] + Spellbook.byID[spellId].Cooldown - time;
        }
    }
}