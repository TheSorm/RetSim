using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class Auras : Dictionary<Aura, AuraState>
    {
        private Player Player { get; init; }

        public Auras(Player player)
        {
            this.Player = player;

            foreach (var aura in Glossaries.Auras.ByID.Values)
            {
                Add(aura, new AuraState());
            }
        }

        public bool IsActive(Aura aura)
        {
            return this[aura].End != null;
        }

        public int GetRemainingDuration(Aura aura, int time)
        {
            if (this[aura].End == null)
                return 0;

            else
                return this[aura].End.ExpirationTime - time;
        }

        public void Apply(Aura aura, int time, List<Event> resultingEvents)
        {
            if (this[aura].Stacks < aura.MaxStacks)
            {
                foreach (AuraEffect effect in aura.Effects)
                {
                    effect.Apply(Player, aura, time, resultingEvents);
                }

                this[aura].Stacks++;
            }

            if (this[aura].End == null)
            {
                this[aura].End = new AuraEndEvent(time + aura.Duration, Player, aura);
                resultingEvents.Add(this[aura].End);
            }

            else
                this[aura].End.ExpirationTime = time + aura.Duration;
        }

        public void Cancel(Aura aura, int time, List<Event> resultingEvents)
        {
            for (int i = 0; i < this[aura].Stacks; i++)
            {
                foreach (AuraEffect effect in aura.Effects)
                {
                    effect.Remove(Player, aura, time, resultingEvents);
                }
            }

            this[aura].Stacks = 0;
            this[aura].End = null;
        }
    }

    public class AuraState
    {
        public AuraEndEvent End { get; set; }
        public int Stacks { get; set; }

        public AuraState()
        {
            End = null;
            Stacks = 0;
        }
    }
}