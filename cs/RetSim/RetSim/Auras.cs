using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class Auras : Dictionary<Aura, AuraEndEvent>
    {
        private Player Player { get; init; }

        public Auras(Player player)
        {
            this.Player = player;

            Add(AuraGlossary.SealOfCommand);
            Add(AuraGlossary.SealOfBlood);
            Add(AuraGlossary.SealOfTheCrusader);
        }
        public new void Add(Aura aura, AuraEndEvent end = null)
        {
            base.Add(aura, null);
        }

        public bool IsActive(Aura aura)
        {
            return this[aura] != null;
        }

        public void Apply(Aura aura, int time, List<Event> resultingEvents)
        {
            foreach (AuraEffect effect in aura.Effects)
            {
                effect.Apply(Player, aura, time, resultingEvents);
            }

            this[aura] = new AuraEndEvent(time + aura.Duration, Player, aura);
            resultingEvents.Add(this[aura]);
        }

        public void Cancle(Aura aura, int time, List<Event> resultingEvents)
        {
            foreach (AuraEffect effect in aura.Effects)
            {
                effect.Remove(Player, aura, time, resultingEvents);
            }

            this[aura] = null;
        }
    }
}
