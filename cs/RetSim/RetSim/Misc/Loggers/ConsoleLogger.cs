using System;

namespace RetSim.Loggers
{
    class ConsoleLogger : AbstractLogger
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}