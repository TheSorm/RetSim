using RetSim.Spells;

namespace RetSim.Data;

public static partial class Spells
{
    public static readonly Spell Relentless = new()
    {
        ID = 39957,
        Name = "Increase Critical Damage 3%"
    };

    public static readonly Spell DragonspineTrophy = new()
    {
        ID = 34774,
        Name = "Dragonspine Trophy"
    };

    public static readonly Spell DragonspineTrophyProc = new()
    {
        ID = 34775,
        Name = "Dragonspine Trophy"
    };

    public static readonly Spell Lionheart = new()
    {
        ID = 34513, //Does not actually exist, need to add Chance on Hit
        Name = "Lionheart"
    };

    public static readonly Spell LionheartProc = new()
    {
        ID = 34512,
        Name = "Lionheart"
    };

    public static readonly Spell LibramOfAvengement = new()
    {
        ID = 34258, //Does not actually exist, need to add Chance on Hit
        Name = "Justice"
    };

    public static readonly Spell LibramOfAvengementProc = new()
    {
        ID = 34260,
        Name = "Justice"
    };

    public static readonly Spell BloodlustBrooch = new()
    {
        ID = 35166,
        Name = "Lust for Battle",
        Cooldown = 120000
    };
}