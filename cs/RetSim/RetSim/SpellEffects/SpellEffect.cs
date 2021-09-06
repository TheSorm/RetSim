namespace RetSim.SpellEffects
{
    public abstract class SpellEffect
    {

        public Spell Spell { get; init; }
        public float MinEffect { get; init; }
        public float MaxEffect { get; init; }

        public abstract ProcMask Resolve(FightSimulation fight);
    }
}