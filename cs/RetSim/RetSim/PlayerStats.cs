namespace RetSim
{
    public class PlayerStats : Stats
    {
        private readonly Player player;

        public Stats Temporary { get; init; }
        private readonly Stats permanent;

        public PlayerStats(Player parent, Race race, Equipment equipment)
        {
            player = parent;
            Temporary = new Stats();
            permanent = race.Stats + equipment.Stats;
        }

        public int MaxHealth => Constants.BaseStats.Health + Stamina * Constants.Stats.HealthPerStamina;
        public int MaxMana => Constants.BaseStats.Mana + Intellect * Constants.Stats.ManaPerIntellect;

        private int health, mana;
        public int Health { get => health; set => health = value <= 0 ? 0 : value <= MaxHealth ? value : MaxHealth; }
        public int Mana { get => mana; set => mana = value <= 0 ? 0 : value <= MaxHealth ? value : MaxHealth; }

        public new int Stamina => (int)((permanent.Stamina + Temporary.Stamina) * player.Modifiers.AllStats);

        public new int Intellect => (int)((permanent.Intellect + Temporary.Intellect) * player.Modifiers.Intellect * player.Modifiers.AllStats);
        public new int ManaPer5 => permanent.ManaPer5 + Temporary.ManaPer5;

        public new int Strength => (int)((permanent.Strength + Temporary.Strength) * player.Modifiers.Strength * player.Modifiers.AllStats);
        public new int AttackPower => (int)((permanent.AttackPower + Temporary.AttackPower + Constants.BaseStats.AttackPower + (Strength * Constants.Stats.APPerStrength)) * player.Modifiers.AttackPower);

        public new int Agility => (int)((permanent.Agility + Temporary.Agility) * player.Modifiers.AllStats);
        public new int CritRating => permanent.CritRating + Temporary.CritRating;
        public new float CritChance => permanent.CritChance + Temporary.CritChance + Constants.BaseStats.CritChance + (Agility / Constants.Stats.AgilityPerCrit) + (CritRating / Constants.Ratings.Crit);

        public new int HitRating => permanent.HitRating + Temporary.HitRating;
        public new float HitChance => permanent.HitChance + Temporary.HitChance + (HitRating / Constants.Ratings.Hit);
        public float HitPenalty => (int)HitChance >= Constants.Stats.HitPenalty ? Constants.Stats.HitPenalty : HitChance;
        public float EffectiveHitChance => HitChance - HitPenalty;

        public new int HasteRating => permanent.HasteRating + Temporary.HasteRating;
        public new float Haste => (Constants.Misc.One + (HasteRating / (Constants.Ratings.Haste * Constants.Misc.OneHundred))) * player.Modifiers.AttackSpeed;

        public new int ExpertiseRating => permanent.ExpertiseRating + Temporary.ExpertiseRating;
        public new int Expertise => permanent.Expertise + Temporary.Expertise + (int)(ExpertiseRating / Constants.Ratings.Expertise);
        public float DodgeChanceReduction => Expertise >= Constants.Stats.ExpertiseCap ? Constants.Stats.ExpertiseCap / Constants.Stats.ExpertisePerDodge : Expertise / Constants.Stats.ExpertisePerDodge;

        public new int ArmorPenetration => permanent.ArmorPenetration + Temporary.ArmorPenetration;
        public new int WeaponDamage => permanent.WeaponDamage + Temporary.WeaponDamage;

        public new int SpellPower => permanent.SpellPower + Temporary.SpellPower;

        public new int SpellCritRating => permanent.SpellCritRating + Temporary.SpellCritRating;
        public new float SpellCrit => permanent.SpellCrit + Temporary.SpellCrit + Constants.BaseStats.SpellCritChance + (Intellect / Constants.Stats.IntellectPerSpellCrit) + (SpellCritRating / Constants.Ratings.SpellCrit);

        public new int SpellHitRating => permanent.SpellHitRating + Temporary.SpellHitRating;
        public new float SpellHit => permanent.SpellHit + Temporary.SpellHit + (SpellHitRating / Constants.Ratings.SpellHit);

        public new int SpellHasteRating => permanent.SpellHasteRating + Temporary.SpellHasteRating;
        public new float SpellHaste => (Constants.Misc.One + (SpellHasteRating / (Constants.Ratings.SpellHaste * Constants.Misc.OneHundred))) * player.Modifiers.CastSpeed;
    }
}