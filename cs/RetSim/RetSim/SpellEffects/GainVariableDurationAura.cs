namespace RetSim.SpellEffects
{
    class GainVariableDurationAura : GainAura
    {
        public int Min { get; init; }
        public int Max {  get; init; }


        public GainVariableDurationAura(Aura aura, bool spellcast, int min, int max) : base(aura, spellcast)
        {
            Min = min;
            Max = max;
        }

        protected override void ApplyAura(FightSimulation fight)
        {
            fight.Player.Auras.Apply(Aura, fight, RNG.RollRange(Min, Max));
        }
    }
}
