
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

            public static void Initialize(List<EquippableWeapon> weapons, List<EquippableArmor> armorPieces)
            {
                foreach (var weapon in weapons)
                {
                    WeaponByID.Add(weapon.ID, weapon);
                }

                foreach (var armor in armorPieces)
                {
                    switch (armor.InventoryType)
                    {
                        case "HEAD":
                            HeadsByID.Add(armor.ID,  armor);
                            break;
                        case "NECK":
                            NecksByID.Add(armor.ID,  armor);
                            break;
                        case "SHOULDER":
                            ShouldersByID.Add(armor.ID,  armor);
                            break;
                        case "CLOAK":
                            CloaksByID.Add(armor.ID,  armor);
                            break;
                        case "CHEST":
                            ChestsByID.Add(armor.ID,  armor);
                            break;
                        case "WRIST":
                            WristsByID.Add(armor.ID,  armor);
                            break;
                        case "HAND":
                            HandsByID.Add(armor.ID,  armor);
                            break;
                        case "WAIST":
                            WaistsByID.Add(armor.ID,  armor);
                            break;
                        case "LEGS":
                            LegsByID.Add(armor.ID,  armor);
                            break;
                        case "FEET":
                            FeetsByID.Add(armor.ID,  armor);
                            break;
                        case "FINGER":
                            FingersByID.Add(armor.ID,  armor);
                            break;
                        case "TRINKET":
                            TrinketsByID.Add(armor.ID,  armor);
                            break;
                        case "RELIC":
                            RelicsByID.Add(armor.ID,  armor);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
