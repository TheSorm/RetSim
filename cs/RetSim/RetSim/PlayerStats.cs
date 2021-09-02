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

        public new int Stamina => (int)((permanent.Stamina + Temporary.Stamina) * player.Modifiers.Stats.All);

        public new int Intellect => (int)((permanent.Intellect + Temporary.Intellect) * player.Modifiers.Stats.Intellect * player.Modifiers.Stats.All);
        public new int ManaPer5 => permanent.ManaPer5 + Temporary.ManaPer5;

        public new int Strength => (int)((permanent.Strength + Temporary.Strength) * player.Modifiers.Stats.Strength * player.Modifiers.Stats.All);
        public new int AttackPower => (int)((permanent.AttackPower + Temporary.AttackPower + Constants.BaseStats.AttackPower + (Strength * Constants.Stats.APPerStrength)) * player.Modifiers.Stats.AttackPower);

        public new int Agility => (int)((permanent.Agility + Temporary.Agility) * player.Modifiers.Stats.All);
        public new int CritRating => permanent.CritRating + Temporary.CritRating;
        public new float CritChance => permanent.CritChance + Temporary.CritChance + Constants.BaseStats.CritChance + (Agility / Constants.Stats.AgilityPerCrit) + (CritRating / Constants.Ratings.Crit);

        public float EffectiveCritDamage => Constants.Stats.PhysicalCritBonus;

        public new int HitRating => permanent.HitRating + Temporary.HitRating;
        public new float HitChance => permanent.HitChance + Temporary.HitChance + (HitRating / Constants.Ratings.Hit);
        public float HitPenalty => (int)HitChance >= Constants.Stats.HitPenalty ? Constants.Stats.HitPenalty : HitChance;
        public float EffectiveHitChance => HitChance - HitPenalty;
        public float EffectiveMissChance
        {
            get
            {
                float miss = Constants.Boss.MissChance - EffectiveHitChance;

                return miss > 0 ? miss : 0;
            }
        }

        public new int HasteRating => permanent.HasteRating + Temporary.HasteRating;
        public new float Haste => (Constants.Misc.One + (HasteRating / (Constants.Ratings.Haste * Constants.Misc.OneHundred))) * player.Modifiers.AttackSpeed;

        public new int ExpertiseRating => permanent.ExpertiseRating + Temporary.ExpertiseRating;
        public new int Expertise => permanent.Expertise + Temporary.Expertise + (int)(ExpertiseRating / Constants.Ratings.Expertise);
        public float DodgeChanceReduction => Expertise >= Constants.Stats.ExpertiseCap ? Constants.Stats.ExpertiseCap / Constants.Stats.ExpertisePerDodge : Expertise / Constants.Stats.ExpertisePerDodge;
        public float EffectiveDodgeChance
        {
            get
            {
                float dodge = Constants.Boss.DodgeChance - DodgeChanceReduction;

                return dodge > 0 ? dodge : 0;
            }
        }

        public new int ArmorPenetration => permanent.ArmorPenetration + Temporary.ArmorPenetration;
        public new int WeaponDamage => permanent.WeaponDamage + Temporary.WeaponDamage;

        public new int SpellPower => permanent.SpellPower + Temporary.SpellPower;

        public new int SpellCritRating => permanent.SpellCritRating + Temporary.SpellCritRating;
        public new float SpellCrit => permanent.SpellCrit + Temporary.SpellCrit + Constants.BaseStats.SpellCritChance + (Intellect / Constants.Stats.IntellectPerSpellCrit) + (SpellCritRating / Constants.Ratings.SpellCrit);

        public float EffectiveSpellCritDamage => Constants.Stats.SpellCritBonus;

        public new int SpellHitRating => permanent.SpellHitRating + Temporary.SpellHitRating;
        public new float SpellHit => permanent.SpellHit + Temporary.SpellHit + (SpellHitRating / Constants.Ratings.SpellHit);

        public float EffectiveSpellMissChance
        {
            get
            {
                float miss = Constants.Boss.SpellMissChance - SpellHit;

                return miss > Constants.Boss.MininumSpellMissChance ? Constants.Boss.MininumSpellMissChance : Constants.Misc.Zero;
            }
        }

        public new int SpellHasteRating => permanent.SpellHasteRating + Temporary.SpellHasteRating;
        public new float SpellHaste => (Constants.Misc.One + (SpellHasteRating / (Constants.Ratings.SpellHaste * Constants.Misc.OneHundred))) * player.Modifiers.CastSpeed;
    }
}