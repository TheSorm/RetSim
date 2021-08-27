using System;

namespace RetSim
{
    public static class Helpers
    {
        /// <summary>
        /// Gets the fractional component of a decimal number, up to 2 decimal places, bypassing floating point math. F.e. 42.32512f returns 32.
        /// </summary>
        /// <param name="input">The floating point number to extract the fractional component of.</param>
        /// <returns>The fractional component of the input number, expressed as an integer of up to 2 digits, i.e. 0-99.</returns>
        public static int GetFraction(float input)
        {
            return (int)((decimal)input % Constants.Misc.One * Constants.Misc.OneHundred);
        }

        /// <summary>
        /// Moves the decimal point in a given fraction two places to the left, bypassing floating point math and returning the result as a truncated integer. F.e. 42.32512f returns 4232.
        /// </summary>
        /// <param name="fraction">The fraction to process.</param>
        /// <returns>The truncated integer representation of the processed fraction.</returns>
        public static int UpgradeFraction(float fraction)
        {
            return (int)(fraction * Constants.Misc.OneHundred) + GetFraction(fraction);
        }



        /// <summary>
        /// Converts a given PPM to its respective proc chance %, based on the given player's weapon speed.
        /// </summary>
        /// <param name="ppm">The PPM value of the proc to be converted into % chance.</param>
        /// <param name="player">The player whose weapon to be used to calculate the proc chance.</param>
        /// <returns>The proc chance of the proc in %, expressed as an integer number between 0 and 100.</returns>
        public static float PPMToChance(float ppm, Player player)
        {
            float chance = player.Weapon.BaseSpeed * ppm / 600;

            return chance < Constants.Misc.Zero ? Constants.Misc.Zero : chance > Constants.Misc.OneHundred ? Constants.Misc.OneHundred : chance;
        }
    }
}
