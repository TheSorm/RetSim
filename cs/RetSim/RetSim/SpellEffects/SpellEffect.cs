namespace RetSim.SpellEffects
{
    public abstract class SpellEffect
    {
        public float MinEffect { get; init; }
        public float MaxEffect { get; init; }

        public abstract ProcMask Resolve(FightSimulation fight);
    }
}