namespace RetSim.AuraEffects
{
    abstract class ModifyFlat : AuraEffect
    {
        public int Amount { get; init; }

        protected int CurrentAmount;
        protected int PreviousAmount;
        protected int Difference;

        public override void Apply(Aura aura, FightSimulation fight)
        {
            CurrentAmount = Amount * fight.Player.Auras[aura].Stacks;
            PreviousAmount = CurrentAmount - Amount;
            Difference = CurrentAmount - PreviousAmount;
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            CurrentAmount = PreviousAmount;
            PreviousAmount -= Amount;
            Difference = CurrentAmount - PreviousAmount;
        }
    }
}
