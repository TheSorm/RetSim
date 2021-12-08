using RetSim.Items;
using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.Events;
using RetSim.Spells;
using RetSim.Spells.AuraEffects;
using RetSim.Spells.SpellEffects;
using RetSim.Units.Enemy;
using RetSim.Units.Player.State;
using RetSim.Units.Player.Static;
using RetSim.Units.UnitStats;
using System.Linq;

namespace RetSim.Units.Player;

public class Player : Unit
{
    public Equipment Equipment { get; init; }
    public Weapon Weapon { get; init; }
    public Race Race { get; init; }
    public ShattrathFaction Faction { get; init; }

    public Spellbook Spellbook { get; init; }
    public Procs Procs { get; init; }
    public GCD GCD { get; init; }

    public List<Talent> Talents { get; init; }

    public AutoAttackEvent NextAutoAttack { get; set; }

    private float previousAttackSpeed;

    public List<AuraEndEvent> HasteEndEvents { get; init; }
    public int EffectiveNextAuto { get; set; }

    public void RecalculateNext()
    {
        HasteEndEvents.Sort();

        if (NextAutoAttack == null)
            return;

        int timeOfSwing = NextAutoAttack.Timestamp;

        float currentModifier = Modifiers.AttackSpeed;
        float currentRating = Stats[StatName.HasteRating].Value;
        float currentSpeed = Stats.EffectiveAttackSpeed;

        foreach (AuraEndEvent end in HasteEndEvents)
        {
            if (end.Timestamp < timeOfSwing)
            {
                ModAttackSpeed effect = end.Aura.Effects.First(x => x is ModAttackSpeed) as ModAttackSpeed;

                float newRating = currentRating - effect.HasteRating;
                float newModifier = currentModifier / ModifyPercent.GetDifference(effect.Value, Auras[end.Aura].Stacks);
                float newSpeed = Stats.CalculateEffectiveAttackSpeed(newRating / Constants.Ratings.Haste, newModifier);
                float ratio = currentSpeed / newSpeed;

                int currentRemaining = timeOfSwing - end.Timestamp;
                int realRemaining = (int)(currentRemaining * ratio);

                timeOfSwing = end.Timestamp + realRemaining;

                currentSpeed = newSpeed;
                currentRating = newRating;
                currentModifier = newModifier;
            }

            else
                break;
        }

        EffectiveNextAuto = timeOfSwing;
    }

    public Player(string name, Race race, ShattrathFaction faction, Equipment equipment, List<Talent> talents, StatSet weights = null) : base(name, CreatureType.Humanoid)
    {
        Talents = talents;
        Equipment = equipment;

        if (Equipment.Weapon is null)
            Equipment.Weapon = new EquippableWeapon()
            { ID = 0, Name = "Unarmed", MinDamage = 1, MaxDamage = 1, AttackSpeed = 2000, Type = WeaponType.Unarmed, ItemLevel = 0, Phase = 1, Quality = Quality.Legendary, Slot = Slot.Weapon };

        Race = race;
        Faction = faction;
        Stats = new PlayerStats(this, weights);

        Spellbook = new Spellbook();
        Modifiers = new Modifiers();
        Weapon = new Weapon(this);

        Auras = new Auras();
        Procs = new Procs(this);
        GCD = new GCD();

        foreach (Aura aura in Data.Collections.Auras.Values)
            Auras.Add(aura);

        previousAttackSpeed = Weapon.EffectiveSpeed;

        HasteEndEvents = new();
    }

    public override ProcMask Cast(Spell spell, FightSimulation fight)
    {
        ProcMask mask = ProcMask.None;

        Unit target = GetSpellTarget(spell.Target, fight);

        SpellState state = fight.Player.Spellbook[spell.ID];

        if (state.EffectiveCooldown > 0)
            fight.Queue.Add(new CooldownEndEvent(state, fight, fight.Timestamp + state.EffectiveCooldown));

        if (spell.GCD != null)
        {
            if (spell.GCD.Category == AttackCategory.Spell)
                fight.Queue.Add(new GCDEndEvent(fight, fight.Timestamp + fight.Player.Stats.EffectiveGCD(spell)));

            else
                fight.Queue.Add(new GCDEndEvent(fight, fight.Timestamp + spell.GCD.Duration));
        }

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
            SpellTarget.Enemy => fight.Enemy,
            _ => this
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

    public void RecalculateAttack(FightSimulation fight)
    {
        float speed = Stats.EffectiveAttackSpeed;

        if (NextAutoAttack != null)
        {
            float ratio = previousAttackSpeed / speed;
            float remaining = (NextAutoAttack.Timestamp - fight.Timestamp) * ratio;

            NextAutoAttack.Timestamp = fight.Timestamp + (int)remaining;
        }

        previousAttackSpeed = speed;
    }

    public override string ToString()
    {
        return Name;
    }
}

public enum ShattrathFaction
{
    None = 0,
    Aldor = 1,
    Scryer = 2
}
