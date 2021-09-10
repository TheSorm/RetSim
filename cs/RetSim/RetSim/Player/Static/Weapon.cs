namespace RetSim
{
    public class Weapon
    {
        private readonly Player player;

        public int MinDamage { get; private set; }
        public int MaxDamage { get; private set; }
        public int BaseSpeed { get; private set; }

        public int EffectiveSpeed => (int)(BaseSpeed / player.Stats.EffectiveAttackSpeed);

        public float APBonus => player.Stats[StatName.AttackPower].Value * BaseSpeed / Constants.Numbers.MillisecondsPerSec / Constants.Stats.APPerDPS;
        public float NormalizedAPBonus => player.Stats[StatName.AttackPower].Value * Constants.Stats.NormalizedWeaponSpeed / Constants.Numbers.MillisecondsPerSec / Constants.Stats.APPerDPS;

        public Weapon(Player owner)
        {
            player = owner;

            MinDamage = player.Equipment.Weapon.MinDamage;
            MaxDamage = player.Equipment.Weapon.MaxDamage;
            BaseSpeed = player.Equipment.Weapon.AttackSpeed;
        }

        public float Attack()
        {
            return (RNG.RollRange(MinDamage, MaxDamage) + APBonus) * player.Modifiers.WeaponDamage;
        }

        public float NormalizedAttack()
        {
            return (RNG.RollRange(MinDamage, MaxDamage) + NormalizedAPBonus) * player.Modifiers.WeaponDamage;
        }
    }
}