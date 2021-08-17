using System;

namespace RetSim
{
    class ConsoleLogger : AbstractLogger
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}