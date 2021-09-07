namespace RetSim.AuraEffects
{
    class GainSeal : AuraEffect
    {
        private Seal Seal { get; set; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            Seal = (Seal)aura;

            foreach (Seal other in Seal.ExclusiveWith)
            {
                if (fight.Player.Auras.IsActive(other))
                {
                    if (other.Persist == 0)
                        fight.Player.Auras[other].End.Timestamp = fight.Timestamp;

                    else
                        fight.Player.Auras[other].End.Timestamp = fight.Timestamp + other.Persist;
                }
            }

            fight.Player.Auras.CurrentSeal = Seal;
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            if (fight.Player.Auras.CurrentSeal != null && fight.Player.Auras.CurrentSeal == Seal)
                fight.Player.Auras.CurrentSeal = null;
        }
    }
}
