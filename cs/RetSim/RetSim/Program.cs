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

            Heads[29073].Socket1.SocketedGem = MetaGems[32409];
            Heads[29073].Socket2.SocketedGem = strength;
            Shoulders[29075].Socket1.SocketedGem = strength;
            Shoulders[29075].Socket2.SocketedGem = crit;
            Cloaks[24259].Socket1.SocketedGem = strength;
            Chests[29071].Socket1.SocketedGem = strength;
            Chests[29071].Socket2.SocketedGem = strength;
            Chests[29071].Socket3.SocketedGem = strength;
            Wrists[28795].Socket1.SocketedGem = strength;
            Wrists[28795].Socket2.SocketedGem = stamina;
            Waists[28779].Socket1.SocketedGem = strength;
            Waists[28779].Socket2.SocketedGem = stamina;
            Feet[28608].Socket1.SocketedGem = strength;
            Feet[28608].Socket2.SocketedGem = crit;


            Equipment equipment = new()
            {
                Head = Heads[29073],
                Neck = Necks[29381],
                Shoulders = Shoulders[29075],
                Back = Cloaks[24259],
                Chest = Chests[29071],
                Wrists = Wrists[28795],
                Hands = Hands[30644],
                Waist = Waists[28779],
                Legs = Legs[31544],
                Feet = Feet[28608],
                Finger1 = Fingers[30834],
                Finger2 = Fingers[28757],
                Trinket1 = Trinkets[29383],
                Trinket2 = Trinkets[28830],
                Relic = Relics[27484],
                Weapon = Weapons[28429],
            };

            FightSimulation fight = new(new Player(Races.Human, equipment), new Enemy(Armor.Warrior), new EliteTactic(), 35000, 40000);

            fight.Run();

            fight.Output();

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
