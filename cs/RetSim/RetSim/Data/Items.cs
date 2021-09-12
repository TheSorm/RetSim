using RetSim.Items;

namespace RetSim.Data
{
    public static class Items
    {
        public static readonly Dictionary<int, EquippableItem> AllItems = new();
        public static readonly Dictionary<int, EquippableWeapon> Weapons = new();
        public static readonly Dictionary<int, EquippableItem> Heads = new();
        public static readonly Dictionary<int, EquippableItem> Necks = new();
        public static readonly Dictionary<int, EquippableItem> Shoulders = new();
        public static readonly Dictionary<int, EquippableItem> Cloaks = new();
        public static readonly Dictionary<int, EquippableItem> Chests = new();
        public static readonly Dictionary<int, EquippableItem> Wrists = new();
        public static readonly Dictionary<int, EquippableItem> Hands = new();
        public static readonly Dictionary<int, EquippableItem> Waists = new();
        public static readonly Dictionary<int, EquippableItem> Legs = new();
        public static readonly Dictionary<int, EquippableItem> Feet = new();
        public static readonly Dictionary<int, EquippableItem> Fingers = new();
        public static readonly Dictionary<int, EquippableItem> Trinkets = new();
        public static readonly Dictionary<int, EquippableItem> Relics = new();
        public static readonly Dictionary<int, ItemSet> Sets = new();
        public static readonly Dictionary<int, Gem> Gems = new();
        public static readonly Dictionary<int, MetaGem> MetaGems = new();

        public static void Initialize(List<EquippableWeapon> weapons, List<EquippableItem> armorPieces, List<ItemSet> sets, List<Gem> gems, List<MetaGem> metaGems)
        {
            foreach (var weapon in weapons)
            {
                Weapons.Add(weapon.ID, weapon);
                AllItems.Add(weapon.ID, weapon);
            }

            foreach (var armor in armorPieces)
            {
                AllItems.Add(armor.ID, armor);

                switch (armor.Slot)
                {
                    case Slot.Head:
                        Heads.Add(armor.ID, armor);
                        break;
                    case Slot.Neck:
                        Necks.Add(armor.ID, armor);
                        break;
                    case Slot.Shoulders:
                        Shoulders.Add(armor.ID, armor);
                        break;
                    case Slot.Back:
                        Cloaks.Add(armor.ID, armor);
                        break;
                    case Slot.Chest:
                        Chests.Add(armor.ID, armor);
                        break;
                    case Slot.Wrists:
                        Wrists.Add(armor.ID, armor);
                        break;
                    case Slot.Hands:
                        Hands.Add(armor.ID, armor);
                        break;
                    case Slot.Waist:
                        Waists.Add(armor.ID, armor);
                        break;
                    case Slot.Legs:
                        Legs.Add(armor.ID, armor);
                        break;
                    case Slot.Feet:
                        Feet.Add(armor.ID, armor);
                        break;
                    case Slot.Finger:
                        Fingers.Add(armor.ID, armor);
                        break;
                    case Slot.Trinket:
                        Trinkets.Add(armor.ID, armor);
                        break;
                    case Slot.Relic:
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
