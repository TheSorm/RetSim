namespace RetSim
{
    public class PlayerStats : Stats
    {
        private Player player;

        public Stats Temporary { get; set; }
        private readonly Stats permanent;

        public PlayerStats(Player parent, Race race, Equipment equipment)
        {
            player = parent;
            Temporary = new Stats();
            permanent = race.Stats + equipment.Stats;
        }

        public int MaxHealth { get { return Constants.BaseStats.Health + Stamina * Constants.Stats.HealthPerStamina; } }
        public int MaxMana { get { return Constants.BaseStats.Mana + Intellect * Constants.Stats.ManaPerIntellect; } }

        private int health, mana;
        public int Health { get { return health; } set { health = value <= 0 ? 0 : value <= MaxHealth ? value : MaxHealth; } }
        public int Mana { get { return mana; } set { mana = value <= 0 ? 0 : value <= MaxHealth ? value : MaxHealth; } }

        public new int Stamina { get { return (int)((permanent.Stamina + Temporary.Stamina) * player.Modifiers.AllStats); } }

        public new int Intellect { get { return (int)((permanent.Intellect + Temporary.Intellect) * player.Modifiers.Intellect * player.Modifiers.AllStats); } }
        public new int ManaPer5 { get { return permanent.ManaPer5 + Temporary.ManaPer5; } }

        public new int Strength { get { return (int)((permanent.Strength + Temporary.Strength) * player.Modifiers.Strength * player.Modifiers.AllStats); } }
        public new int AttackPower { get { return (int)((permanent.AttackPower + Temporary.AttackPower + Constants.BaseStats.AttackPower + Strength * Constants.Stats.APPerStrength) * player.Modifiers.AttackPower); } }

        public new int Agility { get { return (int)((permanent.Agility + Temporary.Agility) * player.Modifiers.AllStats); } }
        public new int CritRating { get { return permanent.CritRating + Temporary.CritRating; } }
        public new float CritChance { get { return permanent.CritChance + Temporary.CritChance + Constants.BaseStats.CritChance + (Agility / Constants.Stats.AgilityPerCrit) + (CritRating / Constants.Ratings.Crit); } }

        public new int HitRating { get { return permanent.HitRating + Temporary.HitRating; } }
        public new float HitChance { get { return permanent.HitChance + Temporary.HitChance + (HitRating / Constants.Ratings.Hit); } }
        public float HitPenalty { get { return (int)HitChance >= Constants.Stats.HitPenalty ? Constants.Stats.HitPenalty : HitChance; } }
        public float EffectiveHitChance { get { return HitChance - HitPenalty; } }

        public new int HasteRating { get { return permanent.HasteRating + Temporary.HasteRating; } }
        public new float Haste { get { return (Constants.Misc.One + (HasteRating / (Constants.Ratings.Haste * Constants.Misc.OneHundred))) * player.Modifiers.AttackSpeed; } }

        public new int ExpertiseRating { get { return permanent.ExpertiseRating + Temporary.ExpertiseRating; } }
        public new int Expertise { get { return permanent.Expertise + Temporary.Expertise + (int)(ExpertiseRating / Constants.Ratings.Expertise); } }
        public float DodgeChanceReduction { get { return Expertise >= Constants.Stats.ExpertiseCap ? Constants.Stats.ExpertiseCap / Constants.Stats.ExpertisePerDodge : Expertise / Constants.Stats.ExpertisePerDodge; } }

        public new int ArmorPenetration { get { return permanent.ArmorPenetration + Temporary.ArmorPenetration; } }
        public new int WeaponDamage { get { return permanent.WeaponDamage + Temporary.WeaponDamage; } }

        public new int SpellPower { get { return permanent.SpellPower + Temporary.SpellPower; } }

        public new int SpellCritRating { get { return permanent.SpellCritRating + Temporary.SpellCritRating; } }
        public new float SpellCrit { get { return permanent.SpellCrit + Temporary.SpellCrit + Constants.BaseStats.SpellCritChance + (Intellect / Constants.Stats.IntellectPerSpellCrit) + (SpellCritRating / Constants.Ratings.SpellCrit); } }

        public new int SpellHitRating { get { return permanent.SpellHitRating + Temporary.SpellHitRating; } }
        public new float SpellHit { get { return permanent.SpellHit + Temporary.SpellHit + (SpellHitRating / Constants.Ratings.SpellHit); } }

        public new int SpellHasteRating { get { return permanent.SpellHasteRating + Temporary.SpellHasteRating; } }
        public new float SpellHaste { get { return (Constants.Misc.One + (SpellHasteRating / (Constants.Ratings.SpellHaste * Constants.Misc.OneHundred))) * player.Modifiers.CastSpeed; } }
    }
}