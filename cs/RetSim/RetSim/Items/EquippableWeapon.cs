namespace RetSim.Items;

public class EquippableWeapon : EquippableItem
{
    public WeaponType Type { get; init; }
    public int MinDamage { get; init; }
    public int MaxDamage { get; init; }
    public int AttackSpeed { get; init; }
    public float DPS { get; init; }

    public override string ToString()
    {
        return base.ToString();
    }
}

public enum WeaponType
{
    Unarmed = 0,
    Sword = 1,
    Mace = 2,
    Axe = 3,
    Polearm = 4
}