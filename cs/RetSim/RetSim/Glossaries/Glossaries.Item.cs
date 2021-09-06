using RetSim.Items;
using System.Collections.Generic;

namespace RetSim
{
    public static partial class Glossaries
    {
        public static class Items
        {
            public static readonly Dictionary<int, EquippableWeapon> Weapons = new();
            public static readonly Dictionary<int, EquippableArmor> Heads = new();
            public static readonly Dictionary<int, EquippableArmor> Necks = new();
            public static readonly Dictionary<int, EquippableArmor> Shoulders = new();
            public static readonly Dictionary<int, EquippableArmor> Cloaks = new();
            public static readonly Dictionary<int, EquippableArmor> Chests = new();
            public static readonly Dictionary<int, EquippableArmor> Wrists = new();
            public static readonly Dictionary<int, EquippableArmor> Hands = new();
            public static readonly Dictionary<int, EquippableArmor> Waists = new();
            public static readonly Dictionary<int, EquippableArmor> Legs = new();
            public static readonly Dictionary<int, EquippableArmor> Feet = new();
            public static readonly Dictionary<int, EquippableArmor> Fingers = new();
            public static readonly Dictionary<int, EquippableArmor> Trinkets = new();
            public static readonly Dictionary<int, EquippableArmor> Relics = new();
            public static readonly Dictionary<int, ItemSet> Sets = new();
            public static readonly Dictionary<int, Gem> Gems = new();
            public static readonly Dictionary<int, MetaGem> MetaGems = new();

            public static void Initialize(List<EquippableWeapon> weapons, List<EquippableArmor> armorPieces, List<ItemSet> sets, List<Gem> gems, List<MetaGem> metaGems)
            {
                foreach (var weapon in weapons)
                {
                    Weapons.Add(weapon.ID, weapon);
                }

                foreach (var armor in armorPieces)
                {
                    switch (armor.InventoryType)
                    {
                        case "HEAD":
                            Heads.Add(armor.ID, armor);
                            break;
                        case "NECK":
                            Necks.Add(armor.ID, armor);
                            break;
                        case "SHOULDER":
                            Shoulders.Add(armor.ID, armor);
                            break;
                        case "CLOAK":
                            Cloaks.Add(armor.ID, armor);
                            break;
                        case "CHEST":
                            Chests.Add(armor.ID, armor);
                            break;
                        case "WRIST":
                            Wrists.Add(armor.ID, armor);
                            break;
                        case "HAND":
                            Hands.Add(armor.ID, armor);
                            break;
                        case "WAIST":
                            Waists.Add(armor.ID, armor);
                            break;
                        case "LEGS":
                            Legs.Add(armor.ID, armor);
                            break;
                        case "FEET":
                            Feet.Add(armor.ID, armor);
                            break;
                        case "FINGER":
                            Fingers.Add(armor.ID, armor);
                            break;
                        case "TRINKET":
                            Trinkets.Add(armor.ID, armor);
                            break;
                        case "RELIC":
                            Relics.Add(armor.ID, armor);
                            break;
                        default:
                            break;
                    }
                }

                foreach (var set in sets)
                {
                    Sets.Add(set.ID, set);
                }

                foreach (var gem in gems)
                {
                    Gems.Add(gem.ID, gem);
                }

                foreach (var metaGem in metaGems)
                {
                    MetaGems.Add(metaGem.ID, metaGem);
                }
            }
        }
    }
}
