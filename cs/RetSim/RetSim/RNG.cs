﻿using System;
using System.Collections.Generic;

namespace RetSim
{
    public static class RNG
    {
        private static readonly Random generator = new();

        private static bool Roll(int input, int limit)
        {
            if (input <= Constants.Numbers.Zero)
                return false;

            else if (input >= limit)
                return true;

            else
                return generator.Next(0, limit) < input;
        }

        public static bool Roll100(int input)
        {
            return Roll(input, Constants.Numbers.OneHundred);
        }

        public static bool Roll100(float input)
        {
            int integer = Helpers.UpgradeFraction(input);

            return Roll(integer, 10000);
        }

        /// <summary>
        /// Converts a decimal damage value to integer by randomly rolling against its first 2 decimal places. F.e. 1024.75 has a 75% chance of returning 1025 and a 25% chance of returning 1024.
        /// </summary>
        /// <param name="damage">The damage that would be dealt, as a decimal number.</param>
        /// <returns>The damage value that should be dealt, expressed as a randomly rolled integer.</returns>
        public static int RollDamage(float damage)
        {
            int fraction = Helpers.GetFraction(damage);

            int random = Roll100(fraction) ? Constants.Numbers.One : Constants.Numbers.Zero;

            return (int)damage + random;
        }

        public static float RollGlancing()
        {
            return RollRange(Constants.Boss.GlancePenaltyMin * 100, Constants.Boss.GlancePenaltyMax * 100) / 10000f;
        }

        public static float RollPartialResist()
        {
            int random = RollRange(1, 100);

            float result = 1f;

            foreach (KeyValuePair<float, int> value in Constants.Boss.ResistanceProbabilities)
            {
                if (random <= value.Value)
                {
                    result = value.Key;
                    break;
                }
            }

            return result;
        }

        public static int RollRange(int min, int max)
        {
            return generator.Next(min, max + 1);
        }

        public static float RollRange(float min, float max)
        {
            return RollRange((int)(min * 100), (int)(max * 100)) / 100f;
        }
    }
}