namespace RetSim
{
    public class Race
    {
        public string Name { get; set; } = "Fictional Race";
        public BaseStats Stats { get; set; }

        public Race()
        {
            Name = "Fictional Race";
            Stats = new BaseStats();
        }

        public Race(string name, BaseStats stats)
        {
            Name = name;
            Stats = stats;
        }
    }

    public static class Races
    {
        public static Race Human = new Race("Human", new BaseStats(126, 77, 83, 120, 3197, 2673, 190, 0.6f, 3.32f, 5));
    }

    public class BaseStats
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intellect { get; set; }
        public int Stamina { get; set; }

        public int Health { get; set; }
        public int Mana { get; set; }

        public int AttackPower { get; set; }
        public float CritChance { get; set; }
        public int Expertise { get; set; }

        public float SpellCritChance { get; set; }

        public BaseStats()
        {
            Strength = 1;
            Agility = 1;
            Intellect = 1;
            Stamina = 1;

            Health = 1;
            Mana = 1;

            AttackPower = 1;
            CritChance = 0;
            Expertise = 0;

            SpellCritChance = 0;
        }

        public BaseStats(int bStrength, int bAgility, int bIntellect, int bStamina, int bHealth, int bMana, int bAttackPower, float bCritChance, float bSpellCritChance, int bExpertise = 0)
        {
            Strength = bStrength;
            Agility = bAgility;
            Intellect = bIntellect;
            Stamina = bStamina;

            Health = bHealth;
            Mana = bMana;

            AttackPower = bAttackPower;
            CritChance = bCritChance;
            Expertise = bExpertise;

            SpellCritChance = bSpellCritChance;
        }
    }
}