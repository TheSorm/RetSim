namespace RetSim.AuraEffects
{
    abstract class ModifyPercent : AuraEffect
    {
        public int Percentage { get; init; }

        protected int CurrentMod = 100;
        protected float PreviousMod;
        protected float RelativeDifference;

        public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
        {
            CurrentMod = 100 + (Percentage * fight.Player.Auras[aura].Stacks);
            PreviousMod = CurrentMod - Percentage;            
            RelativeDifference = CurrentMod / PreviousMod;
        }

        public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
        {
            CurrentMod = (int)PreviousMod;
            PreviousMod -= Percentage;
            RelativeDifference = CurrentMod / PreviousMod;
        }
    }
}
