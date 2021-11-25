using RetSim.Simulation;
using RetSim.Units.Player;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;

namespace RetSim.Spells.SpellEffects;

[Serializable]
public class WeaponAttack : Damage
{
    public bool Normalized { get; init; }

    public WeaponAttack(float value, float dieSides, School school, float coefficient, DefenseType defense, AttackCategory crit, bool ignoresDefense, ProcMask onCast, ProcMask onHit, ProcMask onCrit, bool normalized) : base(value, dieSides, school, coefficient, defense, crit, ignoresDefense, onCast, onHit, onCrit)
    {
        Normalized = normalized;
    }

    public override float CalculateDamage(Player player, Attack attack, SpellState state)
    {
        School school = School;

        if (CritCategory == AttackCategory.Physical)
            school |= School.Physical;

        float schoolMultiplier = player.Modifiers.DamageDone.GetModifier(school);

        float weapon = player.Weapon.Attack(attack.Enemy.Stats[StatName.IncreasedAttackerAttackPower].Value, Normalized);

        EffectBonus bonus = state.EffectBonuses[state.Spell.Effects.IndexOf(this)];

        return (weapon + GetSpellPowerBonus(player, state) + bonus.Flat) * Value * bonus.Percent * schoolMultiplier;
    }
}