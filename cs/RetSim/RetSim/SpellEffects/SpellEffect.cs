namespace RetSim.SpellEffects
{
    public abstract class SpellEffect
    {
        public abstract ProcMask Resolve(FightSimulation fight);
    }
}