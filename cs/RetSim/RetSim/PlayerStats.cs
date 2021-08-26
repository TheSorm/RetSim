namespace RetSim
{
    public record PlayerStats : Stats
    {
        private Stats TemporaryStats { get; init; }
        private readonly Stats staticStats;

        public PlayerStats(Race race, Equipment equipment)
        {
            TemporaryStats = new Stats();
            staticStats = race.Stats;
            staticStats += equipment.Stats;
            AttackSpeed = equipment.AttackSpeed;
        }

        public int Health { get { return Constants.BaseStats.Health + Stamina * Constants.Stats.HealthPerStamina; } }
        public int Mana { get { return Constants.BaseStats.Mana + Intellect * Constants.Stats.ManaPerIntellect; } }

        private int currentHealth;
        public int CurrentHealth { get { return currentHealth; } set { currentHealth = ((value <= 0) ? 0 : value < Health ? value : Health); } }

        private int currentMana;
        public int CurrentMana { get { return currentMana; } set { currentMana = ((value <= 0) ? 0 : value < Mana ? value : Mana); } }

        public int AttackSpeed { get; init; }

        public new int Stamina { get { return staticStats.Stamina + TemporaryStats.Stamina; } }

        public new int Intellect { get { return staticStats.Intellect + TemporaryStats.Intellect; } }
        public new int ManaPer5 { get { return staticStats.ManaPer5 + TemporaryStats.ManaPer5; } }

        public new int Strength { get { return staticStats.Strength + TemporaryStats.Strength; } }
        public new int AttackPower { get { return staticStats.AttackPower + TemporaryStats.AttackPower; } }

        public new int Agility { get { return staticStats.Agility + TemporaryStats.Agility; } }
        public new float CritChance { get { return staticStats.CritChance + TemporaryStats.CritChance; } }
        public new int CritRating { get { return staticStats.CritRating + TemporaryStats.CritRating; } }

        public new float HitChance { get { return staticStats.HitChance + TemporaryStats.HitChance; } }
        public new int HitRating { get { return staticStats.HitRating + TemporaryStats.HitRating; } }

        public new float Haste { get { return staticStats.Haste + TemporaryStats.Haste; } }
        public new int HasteRating { get { return staticStats.HasteRating + TemporaryStats.HasteRating; } }

        public new int Expertise { get { return staticStats.Expertise + TemporaryStats.Expertise; } }
        public new int ExpertiseRating { get { return staticStats.ExpertiseRating + TemporaryStats.ExpertiseRating; } }

        public new int ArmorPenetration { get { return staticStats.ArmorPenetration + TemporaryStats.ArmorPenetration; } }
        public new int WeaponDamage { get { return staticStats.WeaponDamage + TemporaryStats.WeaponDamage; } }

        public new int SpellPower { get { return staticStats.SpellPower + TemporaryStats.SpellPower; } }

        public new float SpellCrit { get { return staticStats.SpellCrit + TemporaryStats.SpellCrit; } }
        public new int SpellCritRating { get { return staticStats.SpellCritRating + TemporaryStats.SpellCritRating; } }

        public new float SpellHit { get { return staticStats.SpellHit + TemporaryStats.SpellHit; } }
        public new int SpellHitRating { get { return staticStats.SpellHitRating + TemporaryStats.SpellHitRating; } }

        public new float SpellHaste { get { return staticStats.SpellHaste + TemporaryStats.SpellHaste; } }
        public new int SpellHasteRating { get { return staticStats.SpellHasteRating + TemporaryStats.SpellHasteRating; } }

    }
}
