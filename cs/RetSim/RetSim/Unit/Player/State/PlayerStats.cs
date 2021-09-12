namespace RetSim
{
    public class PlayerStats : Stats
    {
        private Player Player { get; init; }

        public PlayerStats(Player player) : base(player)
        {
            Player = player;
            var equipment = Player.Equipment;
            var race = Player.Race;

            StatSet gear = Equipment.CalculateStats(equipment);

            All = new Stat[Enum.GetNames(typeof(StatName)).Length];

            this[StatName.Health] = new IntegerStat(StatName.Health, Constants.BaseStats.Health + race.Stats[StatName.Health], gear[StatName.Health]);
            this[StatName.Stamina] = new IntegerStat(StatName.Stamina, race.Stats[StatName.Stamina], gear[StatName.Stamina], (this[StatName.Health], Constants.Stats.StaminaPerHealth));

            this[StatName.Armor] = new IntegerStat(StatName.Armor, 0, gear[StatName.Armor]);
            this[StatName.Resilience] = new IntegerStat(StatName.Resilience, 0, gear[StatName.Resilience]);

            this[StatName.SpellCrit] = new DecimalStat(StatName.SpellCrit, Constants.BaseStats.SpellCritChance, gear[StatName.SpellCrit]);
            this[StatName.SpellCritRating] = new Rating(StatName.SpellCritRating, 0, gear[StatName.SpellCritRating], this[StatName.SpellCrit], Constants.Ratings.SpellCrit);
            this[StatName.Mana] = new IntegerStat(StatName.Mana, Constants.BaseStats.Mana, gear[StatName.Mana]);
            this[StatName.Intellect] = new IntegerStat(StatName.Intellect, race.Stats[StatName.Intellect], gear[StatName.Intellect], (this[StatName.Mana], Constants.Stats.IntellectPerMana), (this[StatName.SpellCrit], Constants.Stats.IntellectPerSpellCrit));
            
            this[StatName.ManaPer5] = new IntegerStat(StatName.ManaPer5, 0, gear[StatName.ManaPer5]);

            this[StatName.AttackPower] = new IntegerStat(StatName.AttackPower, Constants.BaseStats.AttackPower, gear[StatName.AttackPower]);
            this[StatName.Strength] = new IntegerStat(StatName.Strength, race.Stats[StatName.Strength], gear[StatName.Strength], (this[StatName.AttackPower], Constants.Stats.StrengthPerAP));

            this[StatName.CritChance] = new DecimalStat(StatName.CritChance, Constants.BaseStats.CritChance, gear[StatName.CritChance]);
            this[StatName.CritRating] = new Rating(StatName.CritRating, 0, gear[StatName.CritRating], this[StatName.CritChance], Constants.Ratings.Crit);
            this[StatName.Agility] = new IntegerStat(StatName.Agility, race.Stats[StatName.Agility], gear[StatName.Agility], (this[StatName.CritChance], Constants.Stats.AgilityPerCrit), (this[StatName.Armor], Constants.Stats.AgilityPerArmor));
            

            this[StatName.HitChance] = new DecimalStat(StatName.HitChance, 0, gear[StatName.HitChance]);
            this[StatName.HitRating] = new Rating(StatName.HitRating, 0, gear[StatName.HitRating], this[StatName.HitChance], Constants.Ratings.Hit);

            this[StatName.Haste] = new DecimalStat(StatName.Haste, 0, gear[StatName.Haste]);
            this[StatName.HasteRating] = new Rating(StatName.HasteRating, 0, gear[StatName.HasteRating], this[StatName.Haste], Constants.Ratings.Haste);

            this[StatName.Expertise] = new IntegerStat(StatName.Expertise, 0, gear[StatName.Expertise]);
            this[StatName.ExpertiseRating] = new Rating(StatName.ExpertiseRating, 0, gear[StatName.ExpertiseRating], this[StatName.Expertise], Constants.Ratings.Expertise);

            this[StatName.ArmorPenetration] = new IntegerStat(StatName.ArmorPenetration, 0, gear[StatName.ArmorPenetration]);

            this[StatName.SpellPower] = new IntegerStat(StatName.SpellPower, 0, gear[StatName.SpellPower]);
            
            this[StatName.SpellHit] = new DecimalStat(StatName.SpellHit, 0, gear[StatName.SpellHit]);
            this[StatName.SpellHitRating] = new Rating(StatName.SpellHitRating, 0, gear[StatName.SpellHitRating], this[StatName.SpellHit], Constants.Ratings.SpellHit);

            this[StatName.SpellHaste] = new DecimalStat(StatName.SpellHaste, 0, gear[StatName.SpellHaste]);
            this[StatName.SpellHasteRating] = new Rating(StatName.SpellHasteRating, 0, gear[StatName.SpellHasteRating], this[StatName.SpellHaste], Constants.Ratings.SpellHaste);

            this[StatName.Spirit] = new IntegerStat(StatName.Spirit, 0, gear[StatName.Spirit]);
            this[StatName.WeaponDamage] = new IntegerStat(StatName.WeaponDamage, 0, gear[StatName.WeaponDamage]);

            this[StatName.CritDamage] = new DecimalStat(StatName.CritDamage, Constants.Stats.PhysicalCritBonus, 0);
            this[StatName.SpellCritDamage] = new DecimalStat(StatName.SpellCritDamage, Constants.Stats.SpellCritBonus, 0);
        }
    }
}
