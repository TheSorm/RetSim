using RetSim.Items;
using RetSim.Log;
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
            Glossaries.Items.Initialize(LoadWeponData(), LoadArmorData());

            Equipment equipment = new()
            {
                Head = Glossaries.Items.HeadsByID[32087],
                Neck = Glossaries.Items.NecksByID[29119],
                Shoulder = Glossaries.Items.ShouldersByID[33173],
                Cloak = Glossaries.Items.CloaksByID[24259],
                Chest = Glossaries.Items.ChestsByID[23522],
                Wrist = Glossaries.Items.WristsByID[23537],
                Hand = Glossaries.Items.HandsByID[30644],
                Waist = Glossaries.Items.WaistsByID[27985],
                Legs = Glossaries.Items.LegsByID[31544],
                Feet = Glossaries.Items.FeetsByID[25686],
                Finger1 = Glossaries.Items.FingersByID[30834],
                Finger2 = Glossaries.Items.FingersByID[29177],
                Trinket1 = Glossaries.Items.TrinketsByID[23206],
                Trinket2 = Glossaries.Items.TrinketsByID[28288],
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
    }
}
