namespace RetSim
{
    public class Weapon
    {
        private Player player;

        public int MinDamage { get; private set; }
        public int MaxDamage { get; private set; }
        public int BaseSpeed { get; private set; }

        public int EffectiveSpeed { get { return (int)(BaseSpeed / player.Stats.Haste); } }

        public float APBonus { get { return player.Stats.AttackPower * BaseSpeed / Constants.Misc.MillisecondsPerSec / Constants.Stats.APPerDPS; } }
        public float NormalizedAPBonus { get { return player.Stats.AttackPower * Constants.Stats.NormalizedWeaponSpeed / Constants.Misc.MillisecondsPerSec / Constants.Stats.APPerDPS; } }

        public Weapon(Player owner, int min, int max, int speed)
        {
            player = owner;

            MinDamage = min;
            MaxDamage = max;
            BaseSpeed = speed;
        }

        public float Attack()
        {
            return RNG.RollRange(MinDamage, MaxDamage) + APBonus;
        }

        public float NormalizedAttack()
        {
            return RNG.RollRange(MinDamage, MaxDamage) + NormalizedAPBonus;
        }
    }
}