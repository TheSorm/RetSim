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

    public static EquippableWeapon Unarmed = new EquippableWeapon()
    { ID = 0, Name = "Unarmed", MinDamage = 1, MaxDamage = 1, AttackSpeed = 2000, Type = WeaponType.Unarmed, ItemLevel = 0, Phase = 1, Quality = Quality.Legendary, Slot = Slot.Weapon };
}

public enum WeaponType
{
    Unarmed = 0,
    Sword = 1,
    Mace = 2,
    Axe = 3,
    Polearm = 4
}

