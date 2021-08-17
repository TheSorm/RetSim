namespace RetSim
{
    class Program
    {
        public static AbstractLogger Logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            FightSimulation fightSimulation = new (new Player(), new Enemy(), new EliteTactic(), 30 * 1000);

            double result = fightSimulation.Run();

            Logger.Log("DPS:" + result);            
        }
    }
}
