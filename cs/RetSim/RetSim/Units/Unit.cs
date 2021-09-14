using RetSim.Simulation;
using RetSim.Spells;
using RetSim.Spells.SpellEffects;
using RetSim.Units.Enemy;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;

namespace RetSim.Units;

public class Unit
{
    public string Name { get; init; }
    public CreatureType Type { get; init; }
    public Stats Stats { get; init; }
    public Modifiers Modifiers { get; init; }
    public Auras Auras { get; init; }

    public Unit(string name, CreatureType type)
    {
        Name = name;
        Type = type;
    }

    public virtual ProcMask Cast(Spell spell, FightSimulation fight)
    {
        Unit target = GetSpellTarget(spell.Target, fight);

        if (spell.Aura != null)
            target.Auras.Apply(spell.Aura, this, target, fight);

        if (spell.Effects != null)
        {
            foreach (SpellEffect effect in spell.Effects)
                effect.Resolve(fight, null);
        }

        return ProcMask.None;
    }

    protected virtual Unit GetSpellTarget(SpellTarget target, FightSimulation fight)
    {
        return target switch
        {
            SpellTarget.Self => this,
            SpellTarget.Enemy => fight.Enemy,
            _ => fight.Player
        };
    }
}