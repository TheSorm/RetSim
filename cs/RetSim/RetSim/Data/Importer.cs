using RetSim.Items;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static RetSim.Data.Items;

namespace RetSim.Data
{
    public static class Importer
    {
        public static Equipment GetEquipment()
        {
            var data = LoadData();

            Initialize(data.Weapons, data.Armor, data.Sets, data.Gems, data.MetaGems);

            Gem strength = Gems[24027];
            Gem crit = Gems[24058];
            Gem stamina = Gems[24054];

            return new Equipment()
            {
                Head = EquippableItem.GetItemWithGems(29073, new Gem[] { MetaGems[32409], strength }),
                Neck = EquippableItem.GetItemWithGems(29381, null),
                Shoulders = EquippableItem.GetItemWithGems(29075, new Gem[] { strength, crit }),
                Back = EquippableItem.GetItemWithGems(24259, new Gem[] { strength }),
                Chest = EquippableItem.GetItemWithGems(29071, new Gem[] { strength, strength, strength }),
                Wrists = EquippableItem.GetItemWithGems(28795, new Gem[] { strength, stamina }),
                Hands = EquippableItem.GetItemWithGems(30644, null),
                Waist = EquippableItem.GetItemWithGems(28779, new Gem[] { strength, stamina }),
                Legs = EquippableItem.GetItemWithGems(31544, null),
                Feet = EquippableItem.GetItemWithGems(28608, new Gem[] { strength, crit }),
                Finger1 = EquippableItem.GetItemWithGems(30834, null),
                Finger2 = EquippableItem.GetItemWithGems(28757, null),
                Trinket1 = EquippableItem.GetItemWithGems(29383, null),
                Trinket2 = EquippableItem.GetItemWithGems(28830, null),
                Relic = EquippableItem.GetItemWithGems(27484, null),
                Weapon = Weapons[28429],
            };
        }

        public static (List<EquippableWeapon> Weapons, List<EquippableItem> Armor, List<ItemSet> Sets, List<Gem> Gems, List<MetaGem> MetaGems) LoadData()
        {
            return (LoadWeaponData(), LoadArmorData(), LoadSetData(), LoadGemData(), LoadMetaGemData());

        }

        public static List<EquippableWeapon> LoadWeaponData()
        {
            using StreamReader r = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\weapons.json");
            return JsonSerializer.Deserialize<List<EquippableWeapon>>(r.ReadToEnd());
        }

        public static List<EquippableItem> LoadArmorData()
        {
            using StreamReader r = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\armor.json");
            return JsonSerializer.Deserialize<List<EquippableItem>>(r.ReadToEnd());
        }
        public static List<ItemSet> LoadSetData()
        {
            using StreamReader r = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\sets.json");
            return JsonSerializer.Deserialize<List<ItemSet>>(r.ReadToEnd());
        }

        public static List<Gem> LoadGemData()
        {
            using StreamReader r = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\gems.json");
            return JsonSerializer.Deserialize<List<Gem>>(r.ReadToEnd());
        }
        public static List<MetaGem> LoadMetaGemData()
        {
            using StreamReader r = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\metaGems.json");
            return JsonSerializer.Deserialize<List<MetaGem>>(r.ReadToEnd());
        }
    }
}
