using RetSim.Events;
using System;
using System.Collections.Generic;

namespace RetSim
{
    public partial class Player
    {
        private const int SealOfCommandOverlap = 400;

        private AutoAttackEvent nextAutoAttack;
        private GCDEndEvent gcd;

        private Dictionary<int, CooldownEndEvent> spellIdToCooldownEndEvent = new();
        private Dictionary<int, AuraEndEvent> auraIdToAuraEndEvent = new();

        private static readonly Dictionary<int, Func<int, List<Event>, int>> spellIdToSpellCast = new();

        public Player()
        {
            spellIdToSpellCast.Add(Spellbook.crusaderStrike.ID, (time, resultingEvents) => CastCrusaderStrike(time, resultingEvents));
            spellIdToSpellCast.Add(Spellbook.sealOfTheCrusader.ID, (time, resultingEvents) => CastSealOfTheCrusader(time, resultingEvents));
            spellIdToSpellCast.Add(Spellbook.sealOfCommand.ID, (time, resultingEvents) => CastSealOfCommand(time, resultingEvents));
            spellIdToSpellCast.Add(Spellbook.sealOfBlood.ID, (time, resultingEvents) => CastSealOfBlood(time, resultingEvents));

        }

        public int CastSpell(int spellId, int time, List<Event> resultingEvents)
        {
            int cooldown = Spellbook.ByID[spellId].Cooldown;
            if (cooldown > 0)
            {
                CooldownEndEvent cooldownEnd = new(time + cooldown, this, spellId);
                resultingEvents.Add(cooldownEnd);
                spellIdToCooldownEndEvent[spellId] = cooldownEnd;
            }

            switch (Spellbook.ByID[spellId].GCD)
            {
                case Spellbook.GCDType.Physical:
                    {
                        gcd = new GCDEndEvent(time + Spellbook.DefaultGCDTime, this);
                        resultingEvents.Add(gcd);
                        break;
                    }
                case Spellbook.GCDType.Spell:
                    {
                        gcd = new GCDEndEvent(time + Spellbook.DefaultGCDTime, this);
                        resultingEvents.Add(gcd);
                        break;
                    }
                default: break;
            }

            return spellIdToSpellCast[spellId](time, resultingEvents);
        }

        public void ApplyAura(int auraId, int time, List<Event> resultingEvents)
        {
            AuraEndEvent auraEndEvent = new(time + Auras.ByID[auraId].Duration, this, auraId);
            auraIdToAuraEndEvent[Auras.ByID[auraId].ID] = auraEndEvent;
            resultingEvents.Add(auraEndEvent);
        }

        public int CastCrusaderStrike(int time, List<Event> resultingEvents)
        {
            return 1212;
        }

        public int CastSealOfTheCrusader(int time, List<Event> resultingEvents)
        {
            ChangeSeal(Auras.SealOfTheCrusader.ID, time, resultingEvents);
            return 0;
        }

        public int CastSealOfCommand(int time, List<Event> resultingEvents)
        {
            ChangeSeal(Auras.SealOfCommand.ID, time, resultingEvents);
            return 0;
        }

        public int CastSealOfBlood(int time, List<Event> resultingEvents)
        {
            ChangeSeal(Auras.SealOfBlood.ID, time, resultingEvents);
            return 0;
        }

        private void ChangeSeal(int id, int time, List<Event> resultingEvents)
        {
            foreach (Aura seal in Auras.Seals)
            {
                if (auraIdToAuraEndEvent.ContainsKey(seal.ID))
                {
                    auraIdToAuraEndEvent[seal.ID].ExpirationTime = (seal == Auras.SealOfCommand) ? time + SealOfCommandOverlap : time;
                    break;
                }
            }
            ApplyAura(id, time, resultingEvents);
        }

        public int TimeOfNextSwing()
        {
            return nextAutoAttack != null ? nextAutoAttack.ExpirationTime : -1;
        }

        public int MeleeAttack(int time, List<Event> resultingEvents)
        {
            nextAutoAttack = new AutoAttackEvent(time + 3500, this);
            resultingEvents.Add(nextAutoAttack);
            return 1234;
        }

        public void RemoveGCD()
        {
            gcd = null;
        }

        public bool IsOnGCD()
        {
            return gcd != null;
        }

        public int GetEndOfGCD()
        {
            return gcd != null ? gcd.ExpirationTime : -1;
        }

        public void RemoveCooldownOf(int id)
        {
            spellIdToCooldownEndEvent.Remove(id);
        }

        public bool IsOnCooldown(int id)
        {
            return spellIdToCooldownEndEvent.ContainsKey(id);
        }

        public int GetEndOfCooldown(int id)
        {
            return spellIdToCooldownEndEvent.TryGetValue(id, out CooldownEndEvent cooldownEvent) ? cooldownEvent.ExpirationTime : -1;
        }

        public void RemoveAura(int id)
        {
            spellIdToCooldownEndEvent.Remove(id);
        }

        public bool HasAura(int id)
        {
            return auraIdToAuraEndEvent.ContainsKey(id);
        }
        public int GetEndOfAura(int id)
        {
            return auraIdToAuraEndEvent.TryGetValue(id, out AuraEndEvent auraEvent) ? auraEvent.ExpirationTime : -1;
        }
    }
}

