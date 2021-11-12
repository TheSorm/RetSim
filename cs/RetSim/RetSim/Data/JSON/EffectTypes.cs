namespace RetSim.Data.JSON;

enum SpellEffectType
{
    Damage = 1,
    ExtraAttacks = 2,
    JudgementEffect = 3,
    WeaponAttack = 4
}

enum AuraEffectType
{
    GainProc = 1,
    GainSeal = 2,
    ModAttackSpeed = 3,
    ModDamageCreature = 4,
    ModDamageSchool = 5,
    ModDamageSpell = 6,
    ModDamageTaken = 7,
    GainStatsCreature = 8,
    GainStats = 9,
    ModSpellCritChance = 10,
    ModSpellDamageTaken = 11,
    ModStat = 12
}

enum AuraType
{
    Aura = 1,
    Seal = 2
}