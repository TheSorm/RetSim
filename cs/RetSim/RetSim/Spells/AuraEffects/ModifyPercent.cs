namespace RetSim.Spells.AuraEffects;

abstract class ModifyPercent : AuraEffect
{
    public ModifyPercent(float percent) : base(percent) { }

    public static float GetDifference(float value, int stacks)
    {
        float current = 100 + value * stacks;

        float previous = current - value;

        return current / previous;
    }
}