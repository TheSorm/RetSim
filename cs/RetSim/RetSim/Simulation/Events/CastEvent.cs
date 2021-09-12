namespace RetSim.Events
{
    public class CastEvent : Event
    {
        private const int BasePriority = 3;

        private Spell Spell { get; init; }
        private Unit Caster { get; init; }
        private Unit Target { get; init; }

        public CastEvent(Spell spell, Unit caster, Unit target, FightSimulation fight, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
        {
            Spell = spell;
            Caster = caster;
            Target = target;
        }

        public override ProcMask Execute(object arguments = null)
        {
            return Caster.Cast(Spell, Fight);
        }

        public override string ToString()
        {
            return $"{Caster.Name} casts {Spell.Name} on {Target.Name}";
        }
    }
}