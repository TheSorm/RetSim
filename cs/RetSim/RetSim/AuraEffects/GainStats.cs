namespace RetSim.AuraEffects
{
    public class GainStats : AuraEffect
    {
        public Stats Stats { get; init; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            fight.Player.Stats.Temporary += Stats;
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            fight.Player.Stats.Temporary -= Stats;
        }
    }
}
