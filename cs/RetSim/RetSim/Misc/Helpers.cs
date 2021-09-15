namespace RetSim.Misc;

public static class Helpers
{
    /// <summary>
    /// Gets the fractional component of a decimal number, up to 2 decimal places, bypassing floating point math. F.e. 42.32512f returns 32.
    /// </summary>
    /// <param name="input">The floating point number to extract the fractional component of.</param>
    /// <returns>The fractional component of the input number, expressed as an integer of up to 2 digits, i.e. 0-99.</returns>
    public static int GetFraction(float input)
    {
        //return (int)((decimal)input % 1 * 100);

        return (int)(input * 100 - input);
    }

    /// <summary>
    /// Moves the decimal point in a given fraction two places to the left, bypassing floating point math and returning the result as a truncated integer. F.e. 42.32512f returns 4232.
    /// </summary>
    /// <param name="fraction">The fraction to process.</param>
    /// <returns>The truncated integer representation of the processed fraction.</returns>
    public static int UpgradeFraction(float fraction)
    {
        //float mult = fraction * 100;

        //return (int)mult + (int)(GetFraction(mult) / 100f);

        return (int)(fraction * 100);
    }
}

public static class Extensions
{
    public static float Rounded(this float f)
    {
        return (float)Math.Round(f, 2);
    }

    public static void Merge<TKey, TValue>(this Dictionary<TKey, TValue> target, Dictionary<TKey, TValue> source)
    {
        if (target != null && source != null)
        {
            foreach (var entry in source)
                target.Add(entry.Key, entry.Value);
        }
    }
}