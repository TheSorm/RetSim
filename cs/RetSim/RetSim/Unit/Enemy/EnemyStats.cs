namespace RetSim
{
    public class EnemyStats : Stats
    {
        private Enemy Enemy { get; init; }

        public EnemyStats(Enemy enemy) : base(enemy)
        {
            Enemy = enemy;

            All = new Stat[Enum.GetNames(typeof(StatName)).Length];

            this[StatName.Armor] = new IntegerStat(StatName.Armor, (int)Enemy.ArmorCategory, 0);
        }
    }
}
