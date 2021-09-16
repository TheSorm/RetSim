using RetSim.Spells;
using RetSim.Spells.AuraEffects;
using RetSim.Units.UnitStats;

namespace RetSim.Data;

public static partial class Auras
{
    public static readonly Aura SunderArmor = new()
    {
        IsDebuff = true,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.Armor, -2600 } } } }
    };

    public static readonly Aura ExposeArmor = new()
    {
        IsDebuff = true,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.Armor, -2050 } } } }
    };

    public static readonly Aura ImprovedExposeArmor = new()
    {
        IsDebuff = true,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.Armor, -3075 } } } }
    };

    public static readonly Aura FaerieFire = new()
    {
        IsDebuff = true,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.Armor, -610 } } } }
    };

    public static readonly Aura ImprovedFaerieFire = new()
    {
        IsDebuff = true,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.Armor, -610 }, { StatName.IncreasedAttackerHitChance, 3 } } } }
    };

    public static readonly Aura CurseOfRecklessness = new()
    {
        IsDebuff = true,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.Armor, -800 } } } }
    };

    public static readonly Aura CurseOfTheElements = new()
    {
        IsDebuff = true,
        Effects = new() { new ModDamageTaken { Percent = 10, SchoolMask = School.Fire | School.Frost | School.Shadow | School.Arcane } }
    };

    public static readonly Aura ImprovedCurseOfTheElements = new()
    {
        IsDebuff = true,
        Effects = new() { new ModDamageTaken { Percent = 13, SchoolMask = School.Fire | School.Frost | School.Shadow | School.Arcane } }
    };

    public static readonly Aura JudgementOfWisdom = new()
    {
        IsDebuff = true,
        Effects = new() { }

        //TODO: Add proc
    };

    public static readonly Aura ImprovedHuntersMark = new()
    {
        IsDebuff = true,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.IncreasedAttackerAttackPower, 110 } } } }
    };

    public static readonly Aura ExposeWeakness = new()
    {
        IsDebuff = true
    };

    public static readonly Aura BloodFrenzy = new()
    {
        IsDebuff = true,
        Effects = new() { new ModDamageTaken { Percent = 4, SchoolMask = School.Physical } }
    };

    public static readonly Aura ImprovedShadowBolt = new()
    {
        IsDebuff = true,
        Effects = new() { new ModDamageTaken { Percent = 20, SchoolMask = School.Shadow } }
    };

    public static readonly Aura Misery = new()
    {
        IsDebuff = true,
        Effects = new() { new ModDamageTaken { Percent = 5, SchoolMask = School.Magic } }
    };

    public static readonly Aura ShadowWeaving = new()
    {
        IsDebuff = true,
        Effects = new() { new ModDamageTaken { Percent = 10, SchoolMask = School.Shadow } }
    };

    public static readonly Aura ImprovedScorch = new()
    {
        IsDebuff = true,
        Effects = new() { new ModDamageTaken { Percent = 15, SchoolMask = School.Fire } }
    };
}