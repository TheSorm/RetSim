using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public partial class Player
    {
        public Spellbook Spellbook { get; private set; }
        public Auras Auras { get; private set; }

        public int Mana { get; set; }

        private AutoAttackEvent nextAutoAttack;

        private GCDEndEvent gcd;

        public Player()
        {
            Spellbook = new Spellbook(this);
            Auras = new Auras(this);
            Mana = 5000;
        }

        public void Cast(Spell spell, int time, List<Event> resultingEvents)
        {
            Spellbook.Use(spell, time, resultingEvents);
        }

        public void Apply(Aura aura, int time, List<Event> resultingEvents)
        {
            Auras.Apply(aura, time, resultingEvents);
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
    }
}

