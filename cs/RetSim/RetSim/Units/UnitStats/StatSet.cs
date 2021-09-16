namespace RetSim.Units.UnitStats;

public class StatSet : Dictionary<StatName, float>
{
    public new float this[StatName key]
    {
        get
        {
            if (ContainsKey(key))
                return base[key];

            else
                return 0;
        }

        set
        {
            if (ContainsKey(key))
                base[key] = value;

            else
                Add(key, value);
        }
    }
}

public enum StatName
{
    Stamina = 0,
    Health = 1,
    Armor = 2,
    Resilience = 3,
    Intellect = 4,
    Mana = 5,
    ManaPer5 = 6,
    Strength = 7,
    AttackPower = 8,
    Agility = 9,
    CritChance = 10,
    CritRating = 11,
    HitChance = 12,
    HitRating = 13,
    Haste = 14,
    HasteRating = 15,
    Expertise = 16,
    ExpertiseRating = 17,
    ArmorPenetration = 18,
    SpellPower = 19,
    SpellCrit = 20,
    SpellCritRating = 21,
    SpellHit = 22,
    SpellHitRating = 23,
    SpellHaste = 24,
    SpellHasteRating = 25,
    Spirit = 26,
    WeaponDamage = 27,
    CritDamage = 28,
    SpellCritDamage = 29,
    HealingPower = 30,
    Defense = 31,
    DefenseRating = 32,
    Dodge = 33,
    DodgeRating = 34,
    Parry = 35,
    ParryRating = 36,
    BlockChance = 37,
    BlockRating = 38,
    BlockValue = 39,
    IncreasedAttackerAttackPower = 40,
    IncreasedAttackerHitChance = 41,
    IncreasedAttackerCritChance = 42
}