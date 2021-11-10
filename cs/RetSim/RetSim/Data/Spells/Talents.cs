using RetSim.Spells;

namespace RetSim.Data;

public static partial class Spells
{
    public static readonly Talent ImprovedSealOfTheCrusader = new()
    {
        ID = 20337,
        Name = "Improved Seal of the Crusader",
        Rank = 3,
        Target = SpellTarget.Enemy
    };

    public static readonly Talent Conviction = new()
    {
        ID = 20121,
        Rank = 5,
        Name = "Conviction"
    };

    public static readonly Talent Crusade = new()
    {
        ID = 31868,
        Rank = 3,
        Name = "Crusade"
    };

    public static readonly Talent TwoHandedWeaponSpecialization = new()
    {
        ID = 20113,
        Rank = 3,
        Name = "Two-Handed Weapon Specialization"
    };

    public static readonly Talent SanctityAura = new()
    {
        ID = 20218,
        Name = "Sanctity Aura"
    };

    public static readonly Talent ImprovedSanctityAura = new()
    {
        ID = 31870,
        Rank = 2,
        Name = "Improved Sanctity Aura"
    };

    public static readonly Talent Vengeance = new()
    {
        ID = 20059,
        Rank = 5,
        Name = "Vengeance"
    };

    public static readonly Spell VengeanceProc = new()
    {
        ID = 20055,
        Rank = 5,
        Name = "Vengeance"
    };

    //TODO: Sanctified Judgement

    public static readonly Talent SanctifiedSeals = new()
    {
        ID = 35397,
        Rank = 3,
        Name = "Sanctified Seals"
    };

    public static readonly Talent Fanaticism = new()
    {
        ID = 31883,
        Rank = 5,
        Name = "Fanaticism"
    };

    public static readonly Talent Precision = new()
    {
        ID = 20193,
        Rank = 3,
        Name = "Precision"
    };

    public static readonly Talent DivineStrength = new()
    {
        ID = 20266,
        Rank = 5,
        Name = "Divine Strength"
    };

    public static readonly Talent DivineIntellect = new()
    {
        ID = 20261,
        Rank = 5,
        Name = "Divine Intellect"
    };
}