using RetSim.Events;
using System;
using System.Collections.Generic;

namespace RetSim
{
    public partial class Player
    {
        public Spellbook Spellbook { get; private set; }

        public int Mana { get; set; }

        private const int SealOfCommandOverlap = 400;

        private AutoAttackEvent nextAutoAttack;

        private GCDEndEvent gcd;



        private Dictionary<int, AuraEndEvent> auraIdToAuraEndEvent = new();

        private static readonly Dictionary<int, Func<int, List<Event>, int>> spellIdToSpellCast = new();

        public Player()
        {
            Spellbook = new Spellbook(this);
            Mana = 5000;
        }

        public List<Event> Cast(Spell spell, int time)
        {
            return Spellbook.Use(spell, time);
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

        public void StartGCD(GCDEndEvent gcd)
        {
            this.gcd = gcd;
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

