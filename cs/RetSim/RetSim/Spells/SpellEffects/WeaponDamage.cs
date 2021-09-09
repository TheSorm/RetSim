namespace RetSim.SpellEffects
{
    public class WeaponDamage : DamageEffect
    {
        public float Percentage { get; init; } = 1f;

        public override float GetBaseDamage(Player player, SpellState state)
        {
            return GetWeaponDamage(player) * Percentage * state.EffectBonusPercent + state.EffectBonus;
        }

        protected float GetWeaponDamage(Player player)
        {
            return Normalized ? player.Weapon.NormalizedAttack() : player.Weapon.Attack();
        }
    }
}
