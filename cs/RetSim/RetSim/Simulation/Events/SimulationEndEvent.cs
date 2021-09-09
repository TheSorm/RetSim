namespace RetSim.Events
{
    public class SimulationEndEvent : Event
    {
        private const int BasePriority = 5;

        public SimulationEndEvent(FightSimulation fight, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
        {
        }

        public override ProcMask Execute(object arguments = null)
        {
            Fight.Ongoing = false;

            return ProcMask.None;
        }

        public override string ToString()
        {
            return "Simulation ends";
        }
    }
}
