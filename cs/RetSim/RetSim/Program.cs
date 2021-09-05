using RetSim.Items;
using RetSim.Loggers;
using RetSim.Tactics;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace RetSim
{
    class Program
    {
        public static AbstractLogger Logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            Glossaries.Items.Initialize(LoadWeponData(), LoadArmorData(), LoadSetData(), LoadGemData(), LoadMetaGemData());

            Glossaries.Items.HeadsByID[29073].Socket1.SocketedGem = Glossaries.Items.MetaGemsByID[32409];
            Glossaries.Items.HeadsByID[29073].Socket2.SocketedGem = Glossaries.Items.GemsByID[24027];
            Glossaries.Items.ShouldersByID[29075].Socket1.SocketedGem = Glossaries.Items.GemsByID[24027];
            Glossaries.Items.ShouldersByID[29075].Socket2.SocketedGem = Glossaries.Items.GemsByID[24058];
            Glossaries.Items.CloaksByID[24259].Socket1.SocketedGem = Glossaries.Items.GemsByID[24027];
            Glossaries.Items.ChestsByID[29071].Socket1.SocketedGem = Glossaries.Items.GemsByID[24027];
            Glossaries.Items.ChestsByID[29071].Socket2.SocketedGem = Glossaries.Items.GemsByID[24027];
            Glossaries.Items.ChestsByID[29071].Socket3.SocketedGem = Glossaries.Items.GemsByID[24027];
            Glossaries.Items.WristsByID[28795].Socket1.SocketedGem = Glossaries.Items.GemsByID[24027];
            Glossaries.Items.WristsByID[28795].Socket2.SocketedGem = Glossaries.Items.GemsByID[24054];
            Glossaries.Items.WaistsByID[28779].Socket1.SocketedGem = Glossaries.Items.GemsByID[24027];
            Glossaries.Items.WaistsByID[28779].Socket2.SocketedGem = Glossaries.Items.GemsByID[24054];
            Glossaries.Items.FeetsByID[28608].Socket1.SocketedGem = Glossaries.Items.GemsByID[24027];
            Glossaries.Items.FeetsByID[28608].Socket2.SocketedGem = Glossaries.Items.GemsByID[24058];


            Equipment equipment = new()
            {
                Head = Glossaries.Items.HeadsByID[29073],
                Neck = Glossaries.Items.NecksByID[29381],
                Shoulders = Glossaries.Items.ShouldersByID[29075],
                Back = Glossaries.Items.CloaksByID[24259],
                Chest = Glossaries.Items.ChestsByID[29071],
                Wrists = Glossaries.Items.WristsByID[28795],
                Hands = Glossaries.Items.HandsByID[30644],
                Waist = Glossaries.Items.WaistsByID[28779],
                Legs = Glossaries.Items.LegsByID[31544],
                Feet = Glossaries.Items.FeetsByID[28608],
                Finger1 = Glossaries.Items.FingersByID[30834],
                Finger2 = Glossaries.Items.FingersByID[28757],
                Trinket1 = Glossaries.Items.TrinketsByID[29383],
                Trinket2 = Glossaries.Items.TrinketsByID[28830],
                Relic = Glossaries.Items.RelicsByID[27484],
                Weapon = Glossaries.Items.WeaponByID[28429],
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
