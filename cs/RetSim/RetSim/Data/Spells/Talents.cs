using RetSim.Spells;

namespace RetSim.Data;

public static partial class Spells
{
    public static readonly Talent ImprovedSealOfTheCrusader = new() //TODO: Add increased Holy Spell Power
    {
        ID = 20337,
        Name = "Improved Seal of the Crusader",
        Target = SpellTarget.Enemy
    };

    public static readonly Talent Conviction = new()
    {
        ID = 20121,
        Name = "Conviction"
    };

    public static readonly Talent Crusade = new()
    {
        ID = 31868,
        Name = "Crusade"
    };

    public static readonly Talent TwoHandedWeaponSpecialization = new()
    {
        ID = 20113,
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
        Name = "Improved Sanctity Aura"
    };

    public static readonly Talent Vengeance = new()
    {
        ID = 20059,
        Name = "Vengeance"
    };

    public static readonly Spell VengeanceProc = new()
    {
        ID = 20055,
        Name = "Vengeance"
    };

    //TODO: Sanctified Judgement

    public static readonly Talent SanctifiedSeals = new()
    {
        ID = 35397,
        Name = "Sanctified Seals"
    };

    public static readonly Talent Fanaticism = new()
    {
        ID = 31883,
        Name = "Fanaticism"
    };

    public static readonly Talent Precision = new()
    {
        ID = 20193,
        Name = "Precision"
    };

    public static readonly Talent DivineStrength = new()
    {
        ID = 20266,
        Name = "Divine Strength"
    };

    public static readonly Talent DivineIntellect = new()
    {
        ID = 20261,
        Name = "Divine Intellect"
    };
}