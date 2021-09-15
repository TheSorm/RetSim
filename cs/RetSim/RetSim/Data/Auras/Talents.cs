using RetSim.Spells;
using RetSim.Spells.AuraEffects;
using RetSim.Units.Enemy;
using RetSim.Units.UnitStats;

namespace RetSim.Data;

public static partial class Auras
{
    public static readonly Aura ImprovedSealOfTheCrusader = new()
    {
        Effects = new()
        {
            new ModifyStats { Stats = new() { { StatName.CritChance, 3 }, { StatName.SpellCrit, 3 } } }
        }
    };

    public static readonly Aura Conviction = new()
    {
        Effects = new()
        {
            new ModifyStats { Stats = new() { { StatName.CritChance, 5 } } }
        }
    };

    public static readonly Aura Crusade = new()
    {
        Effects = new()
        {
            new ModDamageCreature
            {
                Percentage = 3,
                Types = new() { CreatureType.Humanoid, CreatureType.Demon, CreatureType.Undead, CreatureType.Elemental },
                SchoolMask = School.All
            }
        }
    };

    public static readonly Aura TwoHandedWeaponSpecialization = new()
    {
        Effects = new() { new ModDamageSchool { Percentage = 6, SchoolMask = School.Physical } }
    };

    public static readonly Aura SanctityAura = new()
    {
        Effects = new() { new ModDamageSchool { Percentage = 10, SchoolMask = School.Holy } }
    };

    public static readonly Aura ImprovedSanctityAura = new()
    {
        Effects = new() { new ModDamageSchool { Percentage = 2, SchoolMask = School.All } }
    };

    public static readonly Aura SanctifiedSeals = new()
    {
        Effects = new()
        {
            new ModifyStats { Stats = new() { { StatName.CritChance, 3 }, { StatName.SpellCrit, 3 } } }
        }
    };

    public static readonly Aura Vengeance = new()
    {
    };

    public static readonly Aura VengeanceProc = new()
    {
        Duration = 30000,
        MaxStacks = 3,
        Effects = new() { new ModDamageSchool { Percentage = 5, SchoolMask = School.Physical | School.Holy } }
    };

    public static readonly Aura Fanaticism = new()
    {
    };

    public static readonly Aura Precision = new()
    {
        Effects = new()
        {
            new ModifyStats { Stats = new() { { StatName.HitChance, 3 }, { StatName.SpellHit, 3 } } }
        }
    };

    public static readonly Aura DivineStrength = new()
    {
        Effects = new() { new ModStat { Percentage = 10, Stats = new() { StatName.Strength } } }
    };

    public static readonly Aura DivineIntellect = new()
    {
        Effects = new() { new ModStat { Percentage = 10, Stats = new() { StatName.Intellect } } }
    };
}