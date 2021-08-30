namespace RetSim.Events
{
    public class CooldownEndEvent : Event
    {        
        private const int BasePriority = 2;

        private Spell Spell { get; init; }

        public CooldownEndEvent(Spell spell, FightSimulation fight, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
        {
            Spell = spell;

            Fight.Player.Spellbook.StartCooldown(spell, this);
        }

        public override ProcMask Execute(object arguments = null)
        {
            Fight.Player.Spellbook.EndCooldown(Spell);

            return ProcMask.None;
        }

        public override string ToString()
        {
            return $"Cooldown of {Spell.Name} ends";
        }
    }
}