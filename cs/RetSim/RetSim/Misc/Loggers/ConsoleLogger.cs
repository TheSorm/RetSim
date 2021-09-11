using System;

namespace RetSim.Loggers
{
    class ConsoleLogger : AbstractLogger
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }

        public override void DisableInput()
        {
            Console.CursorVisible = false;
        }

        public override void EnableInput()
        {
            Console.CursorVisible = true;
        }
    }
}