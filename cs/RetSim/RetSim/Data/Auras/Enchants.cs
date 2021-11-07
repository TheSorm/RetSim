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
        Effects = new() { new ModifyStats { Stats = new() { { StatName.Haste, 3 }, { StatName.Agility, 120 } } }}
        //TODO: Add ModAttackSpeed effect
    };

    public static readonly Aura Executioner = new()
    {
    };

    public static readonly Aura ExecutionerProc = new()
    {
        Duration = 15000,
        Effects = new() { new ModifyStats { Stats = new() {  { StatName.ArmorPenetration, 840 } } } }
    };

    public static readonly Aura Deathfrost = new()
    {
    };

    public static readonly Aura DeathfrostProc = new()
    {
    };

}