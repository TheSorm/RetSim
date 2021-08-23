using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class Auras : Dictionary<Aura, AuraEndEvent>
    {
        private Player Player { get; init; }
        public Dictionary<Aura, int> Stacks { get; private set; }

    public Auras(Player player)
        {
            this.Player = player;
            Stacks = new();

            foreach (var aura in AuraGlossary.ByID.Values)
            {
                Add(aura);
                Stacks.Add(aura, 0);
            }
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
            if (Stacks[aura] < aura.MaxStacks)
            {
                foreach (AuraEffect effect in aura.Effects)
                {
                    effect.Apply(Player, aura, time, resultingEvents);
                }
                Stacks[aura]++;
            }

            if(this[aura] == null)
            {
                this[aura] = new AuraEndEvent(time + aura.Duration, Player, aura);
                resultingEvents.Add(this[aura]);
            }
            else
            {
                this[aura].ExpirationTime = time + aura.Duration;
            }
        }

        public void Cancle(Aura aura, int time, List<Event> resultingEvents)
        {
            for(int i = 0; i < Stacks[aura]; i++)
            {
                foreach (AuraEffect effect in aura.Effects)
                {
                    effect.Remove(Player, aura, time, resultingEvents);
                }
            }
            Stacks[aura] = 0;
            this[aura] = null;
        }
    }
}
