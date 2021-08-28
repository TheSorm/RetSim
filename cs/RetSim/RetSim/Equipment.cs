namespace RetSim
{
    public record Equipment
    {
        public Stats Stats { get; init; }
        public int AttackSpeed { get; init; }

        public Equipment()
        {
            Stats = new Stats();
            AttackSpeed = 3500;
        }
    }
}
