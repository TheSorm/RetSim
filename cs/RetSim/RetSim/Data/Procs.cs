using RetSim.Spells;

namespace RetSim.Data;

public static class Procs
{
    public static readonly Proc SealOfCommand = new()
    {
        ID = 1,
        ProcMask = ProcMask.OnWhiteAttack,
        PPM = 7,
        Cooldown = 1000
    };

    public static readonly Proc SealOfBlood = new()
    {
        ID = 2,
        ProcMask = ProcMask.OnBasicAttack,
        GuaranteedProc = true
    };

    public static readonly Proc Vengeance = new()
    {
        ID = 3,
        ProcMask = ProcMask.OnCrit,
        GuaranteedProc = true
    };

    public static readonly Proc WindfuryAttack = new()
    {
        ID = 4,
        ProcMask = ProcMask.OnAutoAttack,
        Chance = 20
    };

    public static readonly Proc Mongoose = new()
    {
        ID = 5,
        ProcMask = ProcMask.OnAnyAttack,
        PPM = 1
    };

    public static readonly Proc Executioner = new()
    {
        ID = 6,
        ProcMask = ProcMask.OnAnyAttack,
        PPM = 1
    };

    public static readonly Proc Deathfrost = new() //Possibly two procs?
    {
        ID = 7,
        ProcMask = ProcMask.OnAnyHit,
        Chance = 50,
        Cooldown = 25000
    };

    public static readonly Proc DragonspineTrophy = new()
    {
        ID = 8,
        ProcMask = ProcMask.OnAnyAttack,
        PPM = 1,
        Cooldown = 20000
    };

    public static readonly Proc Lionheart = new()
    {
        ID = 9,
        ProcMask = ProcMask.OnMeleeAttack,
        PPM = 1,
        Cooldown = 1000
    };

    public static readonly Proc LibramOfAvengement = new()
    {
        ID = 10,
        ProcMask = ProcMask.OnJudgement,
        GuaranteedProc = true
    };

    public static readonly Dictionary<int, Proc> ByID;

    static Procs()
    {
        SealOfCommand.Spell = Spells.SealOfCommandProc;
        SealOfBlood.Spell = Spells.SealOfBloodProc;
        Vengeance.Spell = Spells.VengeanceProc;
        WindfuryAttack.Spell = Spells.WindfuryAttack;
        Mongoose.Spell = Spells.MongooseProc;
        Executioner.Spell = Spells.ExecutionerProc;
        Deathfrost.Spell = Spells.DeathfrostProc;
        DragonspineTrophy.Spell = Spells.DragonspineTrophyProc;
        Lionheart.Spell = Spells.LionheartProc;
        LibramOfAvengement.Spell = Spells.LibramOfAvengementProc;

        ByID = new()
        {
            { Spells.SealOfCommand.ID, SealOfCommand },
            { Spells.SealOfBlood.ID, SealOfBlood },
            { Spells.Vengeance.ID, Vengeance },
            { Spells.WindfuryTotem.ID, WindfuryAttack },
            { Spells.Mongoose.ID, Mongoose },
            { Spells.Executioner.ID, Executioner },
            { Spells.Deathfrost.ID, Deathfrost },
            { Spells.DragonspineTrophy.ID, DragonspineTrophy },
            { Spells.Lionheart.ID, Lionheart },
            { Spells.LibramOfAvengement.ID, LibramOfAvengement }
        };
    }
}