namespace RetSim
{
    class PlayerStats : Stats
    {
        private Race race;
        private Equipment equipment;
        private Stats temporary;


        public override int Stamina { get { return race.Stats.Stamina + equipment.Stats.Stamina + temporary.Stamina; } }
        public override int Health { get { return Constants.BaseStats.Health + Stamina * Constants.Stats.HealthPerStamina; } }

        private int currentHealth;
        public int CurrentHealth { get { return currentHealth; } set { currentHealth = value <= 0 ? 0 : value < Health ? value : Health; } }


    }
}
