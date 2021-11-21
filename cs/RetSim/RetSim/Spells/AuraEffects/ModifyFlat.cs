namespace RetSim.Spells.AuraEffects;

abstract class ModifyFlat : AuraEffect
{
    public ModifyFlat(float amount) : base(amount) { }

    protected static float GetDifference(float value, int stacks)
    {
        float current = value * stacks;

        float previous = current - value;

        return current - previous;
    }
}