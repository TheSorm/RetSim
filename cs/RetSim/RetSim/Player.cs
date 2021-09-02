using RetSim.Events;

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
        public GCD GCD { get; init; }

        public AutoAttackEvent NextAutoAttack { get; set; }

        public Player(Race race, Equipment equipment)
        {
            Stats = new PlayerStats(this, race, equipment);
            Modifiers = new Modifiers();
            Weapon = new Weapon(this, equipment.Weapon.MinDamage, equipment.Weapon.MaxDamage, equipment.Weapon.AttackSpeed);

            Spellbook = new Spellbook(this);
            Auras = new Auras(this);
            Procs = new Procs(this);
            GCD = new GCD();
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