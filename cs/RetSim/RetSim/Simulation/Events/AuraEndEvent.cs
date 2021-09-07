namespace RetSim.Events
{
    public class AuraEndEvent : Event
    {
        private const int BasePriority = 1;

        private Aura Aura { get; init; }


        public AuraEndEvent(Aura aura, FightSimulation fight, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
        {
            Aura = aura;
        }

        public override ProcMask Execute(object arguments = null)
        {
            Fight.Player.Auras.Cancel(Aura, Fight);

            return ProcMask.None;
        }

        public override string ToString()
        {
            return Aura.Parent.Name + " fades.";
        }
    }
}
