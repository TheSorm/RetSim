namespace RetSim.AuraEffects
{
    class Modify : AuraEffect
    {
        public int Percentage { get; init; }

        protected int CurrentMod = 100;
        protected float PreviousMod;
        protected float RelativeDifference;

        public override void Apply(Aura aura, FightSimulation fight)
        {
            PreviousMod = CurrentMod;
            CurrentMod += Percentage;
            RelativeDifference = CurrentMod / PreviousMod;
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            CurrentMod = (int)PreviousMod;
            PreviousMod -= Percentage;
            RelativeDifference = CurrentMod / PreviousMod;
        }
    }
}
