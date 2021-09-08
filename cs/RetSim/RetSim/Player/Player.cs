using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public partial class Player
    {
        public Stats Stats { get; init; }
        public Modifiers Modifiers { get; init; }

        public Equipment Equipment { get; init; }
        public Weapon Weapon { get; init; }
        public Race Race { get; init; }

        public Spellbook Spellbook { get; init; }
        public Auras Auras { get; init; }
        public Procs Procs { get; init; }
        public GCD GCD { get; init; }

        public List<Talent> Talents { get; init; }
        

        public AutoAttackEvent NextAutoAttack { get; set; }

        public Player(Race race, Equipment equipment, List<Talent> talents)
        {
            Equipment = equipment;
            Race = race;
            Stats = new Stats(this, race, equipment);
            Modifiers = new Modifiers();
            Weapon = new Weapon(this, equipment.Weapon.MinDamage, equipment.Weapon.MaxDamage, equipment.Weapon.AttackSpeed);

            Spellbook = new Spellbook(this);
            Auras = new Auras(this);
            Procs = new Procs(this);
            GCD = new GCD();

            Talents = talents;
        }

        public ProcMask Cast(Spell spell, FightSimulation fight)
        {
            return Spellbook.Use(spell, fight);
        }

        public void CheckForProcs(ProcMask mask, FightSimulation fight)
        {
            Procs.CheckProcs(mask, fight);
        }

        public void Apply(Aura aura, FightSimulation fight)
        {
            Auras.Apply(aura, fight);
        }

        public int TimeOfNextSwing()
        {
            return NextAutoAttack != null ? NextAutoAttack.Timestamp : -1;
        }
    }
}