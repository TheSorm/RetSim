using System.Collections.Generic;

namespace RetSim.Events
{
    public class AuraEndEvent : Event
    {
        private const int AuraEndEventPriority = 1;
        private readonly Aura aura;
        public AuraEndEvent(int expirationTime, Player player, Aura aura) : base(expirationTime, AuraEndEventPriority, player)
        {
            this.aura = aura;
        }

        public override ProcMask Execute(int time, List<Event> resultingEvents)
        {
            player.Auras.Cancel(aura, time, resultingEvents);
            return ProcMask.None;
        }

        public override string ToString()
        {
            return aura.Name + " fades";
        }
    }
}
