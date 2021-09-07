using RetSim.Items;
using System.Collections.Generic;
using System.Net;
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
            using WebClient client = new();

            return (LoadWeaponData(client), LoadArmorData(client), LoadSetData(client), LoadGemData(client), LoadMetaGemData(client));

        }

        public static List<EquippableWeapon> LoadWeaponData(WebClient client)
        {
            return JsonSerializer.Deserialize<List<EquippableWeapon>>(client.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/weapons.json"));
        }

        public static List<EquippableItem> LoadArmorData(WebClient client)
        {
            return JsonSerializer.Deserialize<List<EquippableItem>>(client.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/armor.json"));
        }
        public static List<ItemSet> LoadSetData(WebClient client)
        {
            return JsonSerializer.Deserialize<List<ItemSet>>(client.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/sets.json"));
        }

        public static List<Gem> LoadGemData(WebClient client)
        {
            return JsonSerializer.Deserialize<List<Gem>>(client.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/gems.json"));
        }
        public static List<MetaGem> LoadMetaGemData(WebClient client)
        {
            return JsonSerializer.Deserialize<List<MetaGem>>(client.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/metaGems.json"));
        }
    }
}
