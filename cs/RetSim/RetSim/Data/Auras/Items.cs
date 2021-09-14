using RetSim.Spells;
using RetSim.Spells.AuraEffects;
using RetSim.Units.UnitStats;

namespace RetSim.Data;

public static partial class Auras
{
    public static readonly Aura Relentless = new()
    {
        Effects = new() { new ModStat { Percentage = 3, Stats = new() { StatName.CritDamage, StatName.SpellCritDamage } } }
    };

    public static readonly Aura DragonspineTrophy = new()
    {
    };

    public static readonly Aura DragonspineTrophyProc = new()
    {
        Duration = 10000,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.HasteRating, 325 } } } }
    };

    public static readonly Aura Lionheart = new()
    {
    };

    public static readonly Aura LionheartProc = new()
    {
        Duration = 10000,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.Strength, 100 } } } }
    };

    public static readonly Aura LibramOfAvengement = new()
    {
    };

    public static readonly Aura LibramOfAvengementProc = new()
    {
        Duration = 5000,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.CritRating, 53 } } } }
    };

    public static readonly Aura BloodlustBrooch = new()
    {
        Duration = 20000,
        Effects = new() { new ModifyStats { Stats = new() { { StatName.AttackPower, 278 } } } }
    };
}