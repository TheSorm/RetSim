using RetSim.Events;
using RetSim.SpellEffects;
using System.Collections.Generic;

namespace RetSim
{
    public class Player
    {
        public PlayerStats Stats { get; init; }
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
            Talents = talents;
            Equipment = equipment;
            Race = race;
            Stats = new PlayerStats(this);

            Spellbook = new Spellbook();
            Modifiers = new Modifiers();
            Weapon = new Weapon(this);
            
            Auras = new Auras(this);
            Procs = new Procs(this);
            GCD = new GCD();
        }

        public static ProcMask Cast(Spell spell, FightSimulation fight)
        {
            ProcMask mask = ProcMask.None;

            SpellState state = fight.Player.Spellbook[spell.ID];

            if (spell.Cooldown > 0)
                fight.Queue.Add(new CooldownEndEvent(state, fight, fight.Timestamp + state.EffectiveCooldown));

            if (spell.GCD != null)
                fight.Queue.Add(new GCDEndEvent(fight, fight.Timestamp + spell.GCD.Duration));

            if (spell.Aura != null)
                fight.Player.Auras.Apply(spell.Aura, fight);

            if (spell.Effects != null)
            {
                foreach (SpellEffect effect in spell.Effects)
                    mask |= effect.Resolve(fight, state);
            }

            return mask;
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

        public bool SufficientMana(SpellState state)
        {
            return state.EffectiveManaCost <= Stats[StatName.Mana].Value; //TODO: Fix, implement current HP/mana
        }
    }
}