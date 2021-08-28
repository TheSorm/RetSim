using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class Auras : Dictionary<Aura, AuraState>
    {
        private readonly Player player;

        public Auras(Player parent)
        {
            player = parent;

            foreach (var aura in Glossaries.Auras.ByID.Values)
            {
                Add(aura, new AuraState());
            }
        }

        public bool IsActive(Aura aura)
        {
            return this[aura].Active;
        }

        public int GetRemainingDuration(Aura aura, int time)
        {
            if (!this[aura].Active || this[aura].End == null)
                return 0;

            else
                return this[aura].End.ExpirationTime - time;
        }

        public void Apply(Aura aura, int time, List<Event> results)
        {
            if (this[aura].Active)
            {
                if (this[aura].Stacks < aura.MaxStacks)
                    ApplyEffects(aura, time, results);

                this[aura].End.ExpirationTime = time + aura.Duration;
            }

            else
            {
                ApplyEffects(aura, time, results);

                this[aura].End = new AuraEndEvent(time + aura.Duration, player, aura);

                results.Add(this[aura].End);
            }
        }
        private void ApplyEffects(Aura aura, int time, List<Event> results)
        {
            foreach (AuraEffect effect in aura.Effects)
            {
                effect.Apply(player, aura, time, results);
            }

            this[aura].Stacks++;
        }

        public void Cancel(Aura aura, int time, List<Event> resultingEvents)
        {
            for (int i = 0; i < this[aura].Stacks; i++)
            {
                foreach (AuraEffect effect in aura.Effects)
                {
                    effect.Remove(player, aura, time, resultingEvents);
                }
            }

            this[aura].End = null;
            this[aura].Stacks = 0;
        }
    }

    public class AuraState
    {
        public bool Active { get => Stacks > 0; }
        public AuraEndEvent End { get; set; }
        public int Stacks { get; set; }

        public AuraState()
        {
            End = null;
            Stacks = 0;
        }
    }
}