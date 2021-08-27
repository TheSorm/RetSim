using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public partial class Player
    {
        public PlayerStats Stats { get; init; }
        public Modifiers Modifiers { get; init; }
        public Weapon Weapon { get; init; }

        public Spellbook Spellbook { get; init; }
        public Auras Auras { get; init; }
        public Procs Procs { get; init; }

        private AutoAttackEvent nextAutoAttack;

        private GCDEndEvent gcd;

        public Player(Race race, Equipment equipment)
        {
            Stats = new PlayerStats(this, race, equipment);
            Modifiers = new Modifiers();
            Weapon = new Weapon(this, 341, 513, 3600);

            Spellbook = new Spellbook(this);
            Auras = new Auras(this);
            Procs = new Procs(this);
        }

        public ProcMask Cast(Spell spell, int time, List<Event> results)
        {
            return Spellbook.Use(spell, time, results);
        }

        public void CheckForProcs(ProcMask mask, int time, List<Event> results)
        {
            Procs.CheckProcs(mask, time, results);
        }

        public void Apply(Aura aura, int time, List<Event> results)
        {
            Auras.Apply(aura, time, results);
        }

        public int TimeOfNextSwing()
        {
            return nextAutoAttack != null ? nextAutoAttack.ExpirationTime : -1;
        }

        public ProcMask MeleeAttack(int time, List<Event> results)
        {
            nextAutoAttack = new AutoAttackEvent(time + Weapon.EffectiveSpeed, this);
            results.Add(nextAutoAttack);
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

        public int GetGCDEnd()
        {
            return gcd != null ? gcd.ExpirationTime : -1;
        }
    }
}