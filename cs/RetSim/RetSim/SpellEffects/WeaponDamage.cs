namespace RetSim.SpellEffects
{
    public class WeaponDamage : DamageEffect
    {
        public float Percentage { get; init; } = 1f;

        public override float GetBaseDamage(Player player)
        {
            return GetWeaponDamage(player) * Percentage + player.Modifiers.Bonuses[Parent];
        }

        protected float GetWeaponDamage(Player player)
        {
            return Normalized ? player.Weapon.NormalizedAttack() : player.Weapon.Attack();
        }
    }
}
