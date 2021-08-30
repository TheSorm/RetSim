namespace RetSim.SpellEffects
{
    public class SealChange : GainAura
    {
        public Seal Seal { get; init; }

        public SealChange(Seal seal) : base(seal, true)
        {
            Seal = seal;
        }

        public override ProcMask Resolve(FightSimulation fight)
        {
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

            return base.Resolve(fight);
        }
    }
}
