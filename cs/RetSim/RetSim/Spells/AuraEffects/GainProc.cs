namespace RetSim.AuraEffects
{
    public class GainProc : AuraEffect
    {
        public Proc Proc { get; init; }

        public GainProc(Proc proc) : base()
        {
            Proc = proc;
        }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            fight.Player.Procs.Add(Proc);
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            fight.Player.Procs.Remove(Proc);
        }
    }
}