using RetSim.Spells;
using RetSim.Spells.SpellEffects;

namespace RetSim.Data;

public static partial class Spells
{
    public static readonly Spell Mongoose = new()
    {
        ID = 279840,
        Name = "Mongoose"
    };

    public static readonly Spell MongooseProc = new()
    {
        ID = 28093,
        Name = "Lightning Speed"
    };

    public static readonly Spell Executioner = new()
    {
        ID = 429740,
        Name = "Executioner"
    };

    public static readonly Spell ExecutionerProc = new()
    {
        ID = 42976,
        Name = "Executioner"
    };

    public static readonly Spell Deathfrost = new()
    {
        ID = 46662,
        Name = "Deathfrost"
    };

    public static readonly Spell DeathfrostProc = new()
    {
        ID = 46579,
        Name = "Deathfrost",
        Effects = new()
        {
            new Damage() 
            {
                School = School.Frost,
                DefenseCategory = DefenseType.Magic,
                CritCategory = AttackCategory.Spell,
                OnHit = ProcMask.None, //TODO: Add spell hit hit masks
                OnCrit = ProcMask.OnCrit,
                MinEffect = 150,
                MaxEffect = 150
            }
        }
    };
}