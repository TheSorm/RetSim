using RetSim.Items;
using RetSim.Loggers;
using RetSim.Tactics;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using static RetSim.Glossaries.Items;

namespace RetSim
{
    class Program
    {
        public static AbstractLogger Logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            Initialize(LoadWeponData(), LoadArmorData(), LoadSetData(), LoadGemData(), LoadMetaGemData());

            Gem strength = Gems[24027];
            Gem crit = Gems[24058];
            Gem stamina = Gems[24054];

            Equipment equipment = new()
            {
                Head = EquippableArmor.Get(29073, new Gem[] { MetaGems[32409], strength }),
                Neck = EquippableArmor.Get(29381, null),
                Shoulders = EquippableArmor.Get(29075, new Gem[] { strength, crit }),
                Back = EquippableArmor.Get(24259, new Gem[] { strength }),
                Chest = EquippableArmor.Get(29071, new Gem[] { strength, strength, strength }),
                Wrists = EquippableArmor.Get(28795, new Gem[] { strength, stamina }),
                Hands = EquippableArmor.Get(30644, null),
                Waist = EquippableArmor.Get(28779, new Gem[] { strength, stamina }),
                Legs = EquippableArmor.Get(31544, null),
                Feet = EquippableArmor.Get(28608, new Gem[] { strength, crit }),
                Finger1 = EquippableArmor.Get(30834, null),
                Finger2 = EquippableArmor.Get(28757, null),
                Trinket1 = EquippableArmor.Get(29383, null),
                //Trinket2 = EquippableArmor.Get(28830, null),
                Relic = EquippableArmor.Get(27484, null),
                Weapon = Weapons[28429],
            };

            FightSimulation fight = new(new Player(Races.Human, equipment), new Enemy(Armor.Warrior), new EliteTactic(), 35000, 40000);

            fight.Run();

            fight.Output();

            foreach (EquippableItem item in equipment.PlayerEquipment)
            {
                if (item != null)
                    Logger.Log($"\n{item}");
            }

        }

        public static List<EquippableWeapon> LoadWeponData()
        {
            using WebClient wc = new();
            return JsonSerializer.Deserialize<List<EquippableWeapon>>(wc.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/weapons.json"));
        }

        public static List<EquippableArmor> LoadArmorData()
        {
            using WebClient wc = new();
            return JsonSerializer.Deserialize<List<EquippableArmor>>(wc.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/armor.json"));
        }
        public static List<ItemSet> LoadSetData()
        {
            using WebClient wc = new();
            return JsonSerializer.Deserialize<List<ItemSet>>(wc.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/sets.json"));
        }

        public static List<Gem> LoadGemData()
        {
            using WebClient wc = new();
            return JsonSerializer.Deserialize<List<Gem>>(wc.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/gems.json"));
        }
        public static List<MetaGem> LoadMetaGemData()
        {
            using WebClient wc = new();
            return JsonSerializer.Deserialize<List<MetaGem>>(wc.DownloadString("https://raw.githubusercontent.com/TheSorm/RetSim/main/data/metaGems.json"));
        }
    }
}
