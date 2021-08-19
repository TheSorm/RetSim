namespace RetSim
{
    public static class Constants
    {
        public static class Spells
        {
            public const int GCD = 1500;
        }

        public static class BaseStats
        {
            public const int Health = 3197;
            public const int Mana = 2673;
            
            public const int AttackPower = 190;
            public const float CritChance = 0.6f;
            
            public const float SpellCritChance = 3.32f;
        }

        public static class Stats
        {
            public const int HealthPerStamina = 10;
            public const int ManaPerIntellect = 15;

            public const int APPerDPS = 14;

            public const int APPerStrength = 2;
            public const int AgilityPerCrit = 25;
            public const int IntellectPerSpellCrit = 80;

            public const float DodgePerExpertise = 0.25f;

            public const float PhysicalCritBonus = 1f;
            public const float SpellCritBonus = 0.5f;

            public const float NormalizedWeaponSpeed = 3.3f;
        }

        public static class Ratings
        {
            public const float Crit = 22.08f;

            public const float Hit = 15.77f;

            public const float Haste = 15.77f;

            public const float SpellCrit = 22.08f;

            public const float SpellHit = 12.62f;

            public const float SpellHaste = 15.77f;

            public const float Expertise = 3.93f;

            public const float Resilience = 39.4f;
        }
    }
}