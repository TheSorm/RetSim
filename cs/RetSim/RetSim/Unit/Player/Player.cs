using RetSim.Events;
using RetSim.SpellEffects;

namespace RetSim
{
    public class Player : Unit
    {
        public Equipment Equipment { get; init; }
        public Weapon Weapon { get; init; }
        public Race Race { get; init; }

        public Spellbook Spellbook { get; init; }
        public Procs Procs { get; init; }
        public GCD GCD { get; init; }

        public List<Talent> Talents { get; init; }        

        public AutoAttackEvent NextAutoAttack { get; set; }

        public Player(string name, Race race, Equipment equipment, List<Talent> talents) : base(name, CreatureType.Humanoid)
        {
            Talents = talents;
            Equipment = equipment;
            Race = race;
            Stats = new PlayerStats(this);

            Spellbook = new Spellbook();
            Modifiers = new Modifiers();
            Weapon = new Weapon(this);
            
            Auras = new Auras();
            Procs = new Procs(this);
            GCD = new GCD();

            foreach (Aura aura in Data.Auras.ByID.Values)
                Auras.Add(aura);
        }

        public override ProcMask Cast(Spell spell, FightSimulation fight)
        {
            ProcMask mask = ProcMask.None;

            Unit target = GetSpellTarget(spell.Target, fight);

            SpellState state = fight.Player.Spellbook[spell.ID];

            if (spell.Cooldown > 0)
                fight.Queue.Add(new CooldownEndEvent(state, fight, fight.Timestamp + state.EffectiveCooldown));

            if (spell.GCD != null)
                fight.Queue.Add(new GCDEndEvent(fight, fight.Timestamp + spell.GCD.Duration));

            if (spell.Aura != null)
                target.Auras.Apply(spell.Aura, this, target, fight);

            if (spell.Effects != null)
            {
                foreach (SpellEffect effect in spell.Effects)
                    mask |= effect.Resolve(fight, state);
            }

            return mask;
        }

        protected override Unit GetSpellTarget(SpellTarget target, FightSimulation fight)
        {
            return target switch
            {
                SpellTarget.Self => this,
                SpellTarget.Enemy => fight.Enemy,
                _ => fight.Player
            };
        }

        public void CheckForProcs(ProcMask mask, FightSimulation fight)
        {
            Procs.CheckProcs(mask, fight);
        }

        public int TimeOfNextSwing()
        {
            return NextAutoAttack != null ? NextAutoAttack.Timestamp : -1;
        }

        public bool SufficientMana(SpellState state)
        {
            return state.EffectiveManaCost <= Stats[StatName.Mana].Value; //TODO: Fix, implement current HP/mana
        }

        public void RecalculateAttack(FightSimulation fight, float previousSpeed)
        { 
        }
    }
}