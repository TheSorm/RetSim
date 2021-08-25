using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public partial class Player
    {
        public Spellbook Spellbook { get; private set; }
        public Auras Auras { get; private set; }
        public Procs Procs { get; private set; }

        public int WeaponSpeed { get; set; }

        public int Mana { get; set; }

        private AutoAttackEvent nextAutoAttack;

        private GCDEndEvent gcd;

        public Player()
        {
            Spellbook = new Spellbook(this);
            Auras = new Auras(this);
            Procs = new Procs(this);
            WeaponSpeed = 3500;
            Mana = 5000;
        }

        public ProcMask Cast(Spell spell, int time, List<Event> resultingEvents)
        {
            return Spellbook.Use(spell, time, resultingEvents);
        }

        public void Proc(ProcMask procMask, int time, List<Event> resultingEvents)
        {
            Procs.Proc(procMask, time, resultingEvents);
        }

        public void Apply(Aura aura, int time, List<Event> resultingEvents)
        {
            Auras.Apply(aura, time, resultingEvents);
        }

        public int TimeOfNextSwing()
        {
            return nextAutoAttack != null ? nextAutoAttack.ExpirationTime : -1;
        }

        public ProcMask MeleeAttack(int time, List<Event> resultingEvents)
        {
            nextAutoAttack = new AutoAttackEvent(time + 3500, this);
            resultingEvents.Add(nextAutoAttack);
            return ProcMask.OnMeleeAutoAttack;
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

