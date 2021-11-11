namespace RetSim.Data.JSON;

enum SpellEffectType
{
    Damage = 1,
    ExtraAttacks = 2,
    Judgement = 3,
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
    GainStats = 8,
    ModSpellCritChance = 9,
    ModSpellDamageTaken = 10,
    ModStat = 11
}

enum AuraType
{
    Aura = 1,
    Seal = 2
}