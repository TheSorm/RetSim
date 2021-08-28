namespace RetSim
{
    public class Weapon
    {
        private readonly Player player;

        public int MinDamage { get; private set; }
        public int MaxDamage { get; private set; }
        public int BaseSpeed { get; private set; }

        public int EffectiveSpeed => (int)(BaseSpeed / player.Stats.Haste);

        public float APBonus => player.Stats.AttackPower * BaseSpeed / Constants.Misc.MillisecondsPerSec / Constants.Stats.APPerDPS;
        public float NormalizedAPBonus => player.Stats.AttackPower * Constants.Stats.NormalizedWeaponSpeed / Constants.Misc.MillisecondsPerSec / Constants.Stats.APPerDPS;

        public Weapon(Player owner, int min, int max, int speed)
        {
            player = owner;

            MinDamage = min;
            MaxDamage = max;
            BaseSpeed = speed;
        }

        private int DamageRoll()
        {
            return RNG.RollRange(MinDamage + player.Stats.WeaponDamage, MaxDamage + player.Stats.WeaponDamage);
        }

        public float Attack()
        {
            return DamageRoll() + APBonus;
        }

        public float NormalizedAttack()
        {
            return DamageRoll() + NormalizedAPBonus;
        }
    }
}