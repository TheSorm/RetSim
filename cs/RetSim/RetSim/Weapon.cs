namespace RetSim
{
    public class Weapon
    {
        private readonly Player player;

        public int MinDamage { get; private set; }
        public int MaxDamage { get; private set; }
        public int BaseSpeed { get; private set; }

        public int EffectiveSpeed => (int)(BaseSpeed / player.Stats.Haste);

        public float APBonus => player.Stats.AttackPower * BaseSpeed / Constants.Numbers.MillisecondsPerSec / Constants.Stats.APPerDPS;
        public float NormalizedAPBonus => player.Stats.AttackPower * Constants.Stats.NormalizedWeaponSpeed / Constants.Numbers.MillisecondsPerSec / Constants.Stats.APPerDPS;

        public Weapon(Player owner, int min, int max, int speed)
        {
            player = owner;

            MinDamage = min;
            MaxDamage = max;
            BaseSpeed = speed;
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