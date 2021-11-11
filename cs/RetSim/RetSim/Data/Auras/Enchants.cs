using RetSim.Spells;
using RetSim.Spells.AuraEffects;
using RetSim.Units.UnitStats;

namespace RetSim.Data;

public static partial class Auras
{

    public static readonly Aura Mongoose = new()
    {
    };

    public static readonly Aura MongooseProc = new()
    {
        Duration = 15000,
        Effects = new()
        {
            new GainStats { Stats = new() { { StatName.Agility, 120 } } },
            new ModAttackSpeed { Percent = 3 }
        }
    };

    public static readonly Aura Executioner = new()
    {
    };

    public static readonly Aura ExecutionerProc = new()
    {
        Duration = 15000,
        Effects = new() { new GainStats { Stats = new() {  { StatName.ArmorPenetration, 840 } } } }
    };

    public static readonly Aura Deathfrost = new()
    {
    };

    public static readonly Aura DeathfrostProc = new()
    {
    };

}