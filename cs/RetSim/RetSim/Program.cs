using System;

namespace RetSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Enemy enemy = new();
            Player player = new ();
            FightSimulation fightSimulation = new (player, enemy, new EliteTactic(), 30 * 1000);

            Console.WriteLine("DPS:" + fightSimulation.Run());            
        }
    }
}
