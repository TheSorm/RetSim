namespace RetSim.SpellEffects
{
    public class WeaponDamage : DamageEffect
    {
        public float Percentage { get; init; }

        public override float GetBaseDamage(Player player)
        {
            return GetWeaponDamage(player) * Percentage + player.Modifiers.Bonuses[Spell];
        }

        protected float GetWeaponDamage(Player player)
        {
            return Normalized ? player.Weapon.NormalizedAttack() : player.Weapon.Attack();
        }
    }
}
