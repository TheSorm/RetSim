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

            public const int HitPenalty = 1;

            public const int CritSuppression = 3;
            public const float CritAuraSuppression = 1.8f;

            public const int ExpertiseCap = 26;
            public const int ExpertisePerDodge = 4;

            public const float PhysicalCritBonus = 1f;
            public const float SpellCritBonus = 0.5f;

            public const int NormalizedWeaponSpeed = 3300;
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

        public static class Boss
        {
            public const int Level = 73;

            public const int MissChance = 8;
            public const float DodgeChance = 6.5f;
            public const int GlancingChance = 24;
            public const int UpgradedGlancingChance = 2400;
            public const int GlancePenaltyMax = 35;
            public const int GlancePenaltyMin = 15;
        }

        public static class Misc
        {
            public const int SecondsPerMin = 60;
            public const int MillisecondsPerSec = 1000;
            public const int OneHundred = 100;
            public const int One = 1;
            public const int Zero = 0;

            public const string MillisecondFormatter = "{0:00:00:000}";
        }
    }

    public enum Armor
    {
        None = 0,
        Mage = 3878,
        Netherspite = 5474,
        Level71 = 5714,
        Paladin = 6193,
        Demon = 6792,
        Warrior = 7684,
        VoidReaver = 8806
    }
}