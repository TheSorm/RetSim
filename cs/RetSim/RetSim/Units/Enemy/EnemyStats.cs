using RetSim.Units.UnitStats;

namespace RetSim.Units.Enemy
{
    public class EnemyStats : Stats
    {
        private Enemy Enemy { get; init; }

        public EnemyStats(Enemy enemy) : base(enemy)
        {
            Enemy = enemy;

            All = new Stat[Enum.GetNames(typeof(StatName)).Length];

            this[StatName.Armor] = new IntegerStat(StatName.Armor, (int)Enemy.Boss.ArmorCategory, 0);
            this[StatName.IncreasedAttackerAttackPower] = new IntegerStat(StatName.IncreasedAttackerAttackPower, 0, 0);
            this[StatName.IncreasedAttackerHitChance] = new DecimalStat(StatName.IncreasedAttackerHitChance, 0, 0);
            this[StatName.IncreasedAttackerCritChance] = new DecimalStat(StatName.IncreasedAttackerCritChance, 0, 0);
        }
    }
}
