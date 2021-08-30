namespace RetSim
{
    public abstract class AuraEffect
    {
        public AuraEffect() { }

        public abstract void Apply(Aura aura, FightSimulation fight);
        public abstract void Remove(Aura aura, FightSimulation fight);
    }
}