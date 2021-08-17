namespace RetSim
{
    class Program
    {
        public static AbstractLogger Logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            Enemy enemy = new();
            Player player = new ();
            FightSimulation fightSimulation = new (new Player(), enemy, new EliteTactic(), 30 * 1000);

            double result = fightSimulation.Run();

            Logger.Log("DPS:" + result);            
        }
    }
}
