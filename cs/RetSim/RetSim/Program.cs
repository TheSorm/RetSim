using RetSim.Items;
using RetSim.Loggers;
using RetSim.Tactics;

namespace RetSim
{
    class Program
    {
        public static AbstractLogger Logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            var equipment = Data.Importer.GetEquipment();

            FightSimulation fight = new(new Player(Races.Human, equipment), new Enemy(Armor.Warrior), new EliteTactic(), 180000, 200000);
            //TODO: Add human racial tho


            fight.Run();

            fight.Output();

            foreach (EquippableItem item in equipment.PlayerEquipment)
            {
                if (item != null)
                    Logger.Log($"\n{item}");
            }
        }
    }
}
