using RetSim.Simulation;
using RetSim.Units.Player;
using RetSim.Units.Player.State;

namespace RetSim.Spells.SpellEffects;

public class WeaponDamage : DamageEffect
{
    public float Percentage { get; init; } = 1f;

    public override float CalculateDamage(Player player, Attack attack, SpellState state)
    {
        School school = School;

        if (CritCategory == AttackCategory.Physical)
            school |= School.Physical;

        float schoolMultiplier = player.Modifiers.SchoolModifiers.GetModifier(school);

        return ((GetWeaponDamage(player) + GetSpellPowerBonus(player, state)) * Percentage * state.EffectBonusPercent + state.EffectBonus) * schoolMultiplier;
    }

    protected float GetWeaponDamage(Player player)
    {
        return Normalized ? player.Weapon.NormalizedAttack() : player.Weapon.Attack();
    }
}