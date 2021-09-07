namespace RetSim
{
    public class Stats : StatSet
    {
        private Player Player { get; init; }

        public StatSet Temporary { get; set; }
        private StatSet Permanent { get; init; }

        public Stats(Player parent, Race race, Equipment equipment)
        {
            Player = parent;
            Temporary = new StatSet();
            Permanent = race.Stats + equipment.Stats;
        }

        public int MaxHealth => Constants.BaseStats.Health + Stamina * Constants.Stats.HealthPerStamina;
        public int MaxMana => Constants.BaseStats.Mana + Intellect * Constants.Stats.ManaPerIntellect;

        private int health, mana;
        public new int Health { get => health; set => health = value <= 0 ? 0 : value <= MaxHealth ? value : MaxHealth; }
        public new int Mana { get => mana; set => mana = value <= 0 ? 0 : value <= MaxHealth ? value : MaxHealth; }

        public new int Stamina => (int)((Permanent.Stamina + Temporary.Stamina) * Player.Modifiers.Stats.All);

        public new int Intellect => (int)((Permanent.Intellect + Temporary.Intellect) * Player.Modifiers.Stats.Intellect * Player.Modifiers.Stats.All);
        public new int ManaPer5 => Permanent.ManaPer5 + Temporary.ManaPer5;

        public new int Strength => (int)((Permanent.Strength + Temporary.Strength) * Player.Modifiers.Stats.Strength * Player.Modifiers.Stats.All);
        public new int AttackPower => (int)((Permanent.AttackPower + Temporary.AttackPower + Constants.BaseStats.AttackPower + (Strength * Constants.Stats.APPerStrength)) * Player.Modifiers.Stats.AttackPower);

        public new int Agility => (int)((Permanent.Agility + Temporary.Agility) * Player.Modifiers.Stats.All);
        public new int CritRating => Permanent.CritRating + Temporary.CritRating;
        public new float CritChance => Permanent.CritChance + Temporary.CritChance + Constants.BaseStats.CritChance + (Agility / Constants.Stats.AgilityPerCrit) + (CritRating / Constants.Ratings.Crit);

        public float EffectiveCritDamage => Constants.Stats.PhysicalCritBonus;

        public new int HitRating => Permanent.HitRating + Temporary.HitRating;
        public new float HitChance => Permanent.HitChance + Temporary.HitChance + (HitRating / Constants.Ratings.Hit);
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

        public new int HasteRating => Permanent.HasteRating + Temporary.HasteRating;
        public new float Haste => (1 + (HasteRating / (Constants.Ratings.Haste * 100))) * Player.Modifiers.AttackSpeed;

        public new int ExpertiseRating => Permanent.ExpertiseRating + Temporary.ExpertiseRating;
        public new int Expertise => Permanent.Expertise + Temporary.Expertise + (int)(ExpertiseRating / Constants.Ratings.Expertise);
        public float DodgeChanceReduction => Expertise >= Constants.Stats.ExpertiseCap ? Constants.Stats.ExpertiseCap / Constants.Stats.ExpertisePerDodge : Expertise / Constants.Stats.ExpertisePerDodge;
        public float EffectiveDodgeChance
        {
            get
            {
                float dodge = Constants.Boss.DodgeChance - DodgeChanceReduction;

                return dodge > 0 ? dodge : 0;
            }
        }

        public new int ArmorPenetration => Permanent.ArmorPenetration + Temporary.ArmorPenetration;
        public new int WeaponDamage => Permanent.WeaponDamage + Temporary.WeaponDamage;

        public new int SpellPower => Permanent.SpellPower + Temporary.SpellPower;

        public new int SpellCritRating => Permanent.SpellCritRating + Temporary.SpellCritRating;
        public new float SpellCrit => Permanent.SpellCrit + Temporary.SpellCrit + Constants.BaseStats.SpellCritChance + (Intellect / Constants.Stats.IntellectPerSpellCrit) + (SpellCritRating / Constants.Ratings.SpellCrit);

        public float EffectiveSpellCritDamage => Constants.Stats.SpellCritBonus;

        public new int SpellHitRating => Permanent.SpellHitRating + Temporary.SpellHitRating;
        public new float SpellHit => Permanent.SpellHit + Temporary.SpellHit + (SpellHitRating / Constants.Ratings.SpellHit);

        public float EffectiveSpellMissChance
        {
            get
            {
                float miss = Constants.Boss.SpellMissChance - SpellHit;

                return miss > Constants.Boss.MininumSpellMissChance ? Constants.Boss.MininumSpellMissChance : 0;
            }
        }

        public new int SpellHasteRating => Permanent.SpellHasteRating + Temporary.SpellHasteRating;
        public new float SpellHaste => (1 + (SpellHasteRating / (Constants.Ratings.SpellHaste * 100))) * Player.Modifiers.CastSpeed;
    }
}