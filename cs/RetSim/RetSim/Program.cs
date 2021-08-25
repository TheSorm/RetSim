using RetSim.Loggers;
using RetSim.Tactics;

namespace RetSim
{
    class Program
    {
        public static AbstractLogger Logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            Glossaries.Initialize();

            FightSimulation fightSimulation = new(new Player(), new Enemy(), new EliteTactic(), 35 * 1000);

            double result = fightSimulation.Run();

            Logger.Log("DPS:" + result);
        }
    }
}
