
using RetSim.Items;
using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
    {
        public static class Items
        {
            public static readonly Dictionary<int, EquippableWeapon> WeaponByID = new();
            public static readonly Dictionary<int, EquippableArmor> HeadsByID = new();
            public static readonly Dictionary<int, EquippableArmor> NecksByID = new();
            public static readonly Dictionary<int, EquippableArmor> ShouldersByID = new();
            public static readonly Dictionary<int, EquippableArmor> CloaksByID = new();
            public static readonly Dictionary<int, EquippableArmor> ChestsByID = new();
            public static readonly Dictionary<int, EquippableArmor> WristsByID = new();
            public static readonly Dictionary<int, EquippableArmor> HandsByID = new();
            public static readonly Dictionary<int, EquippableArmor> WaistsByID = new();
            public static readonly Dictionary<int, EquippableArmor> LegsByID = new();
            public static readonly Dictionary<int, EquippableArmor> FeetsByID = new();
            public static readonly Dictionary<int, EquippableArmor> FingersByID = new();
            public static readonly Dictionary<int, EquippableArmor> TrinketsByID = new();
            public static readonly Dictionary<int, EquippableArmor> RelicsByID = new();

            public static void Initialize(List<WowItemData.Weapon> weapons, List<WowItemData.Armor> armorPieces)
            {
                foreach (var weapon in weapons)
                {
                    WeaponByID.Add(weapon.ID, new EquippableWeapon(weapon));
                }

                foreach (var armor in armorPieces)
                {
                    switch (armor.InventoryType)
                    {
                        case "HEAD":
                            HeadsByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "NECK":
                            NecksByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "SHOULDER":
                            ShouldersByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "CLOAK":
                            CloaksByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "CHEST":
                            ChestsByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "WRIST":
                            WristsByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "HAND":
                            HandsByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "WAIST":
                            WaistsByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "LEGS":
                            LegsByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "FEET":
                            FeetsByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "FINGER":
                            FingersByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "TRINKET":
                            TrinketsByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        case "RELIC":
                            RelicsByID.Add(armor.ID, new EquippableArmor(armor));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
