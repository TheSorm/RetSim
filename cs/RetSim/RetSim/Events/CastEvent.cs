namespace RetSim.Events
{
    public class CastEvent : Event
    {
        private const int BasePriority = 3;

        private Spell Spell { get; init; }

        public CastEvent(Spell spell, FightSimulation fight, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
        {
            Spell = spell;
        }

        public override ProcMask Execute(object arguments = null)
        {
            return Fight.Player.Cast(Spell, Fight);
        }

        public override string ToString()
        {
            return "Cast " + Spell.Name;
        }
    }
}