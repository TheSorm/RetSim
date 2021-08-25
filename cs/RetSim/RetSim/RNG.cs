using System;

namespace RetSim
{
    public static class RNG
    {
        private static Random generator = new Random();

        public static bool Roll100(int input)
        {
            if (input <= 0)
                return false;

            else if (input >= 100)
                return true;

            else
                return generator.Next(0, 100) < input;            
        }

        public static bool Roll10000(int input)
        {
            if (input <= 0)
                return false;

            else if (input >= 10000)
                return true;

            else
                return generator.Next(0, 10000) < input;
        }

        public static int RollRange(int min, int max)
        {
            return generator.Next(min, max + 1);
        }

        public static float RollRange(float min, float max)
        {
            return generator.Next((int)(min * 100), (int)(max * 100) + 1) / 100f;
        }
    }
}
