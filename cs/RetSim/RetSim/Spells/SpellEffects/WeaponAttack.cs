using RetSim.Simulation;
using RetSim.Units.Player;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;

namespace RetSim.Spells.SpellEffects;

[Serializable]
public class WeaponAttack : Damage
{
    public bool Normalized { get; init; }
    public float Percentage { get; init; }

    public WeaponAttack(float min, float max, School school, float coefficient, DefenseType defense, AttackCategory crit, bool ignoresDefense, ProcMask onCast, ProcMask onHit, ProcMask onCrit, bool normalized, float percentage) : base(min, max, school, coefficient, defense, crit, ignoresDefense, onCast, onHit, onCrit)
    {
        Normalized = normalized;
        Percentage = percentage;
    }

    public override float CalculateDamage(Player player, Attack attack, SpellState state)
    {
        School school = School;

        if (CritCategory == AttackCategory.Physical)
            school |= School.Physical;

        float schoolMultiplier = player.Modifiers.SchoolDamageDone.GetModifier(school);

        float weapon = player.Weapon.Attack(attack.Enemy.Stats[StatName.IncreasedAttackerAttackPower].Value, Normalized);

        return ((weapon + GetSpellPowerBonus(player, state)) * Percentage * state.EffectBonusPercent + state.EffectBonus) * schoolMultiplier;
    }
}