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
                return this[aura].End.Timestamp - time;
        }

        public void Apply(Aura aura, FightSimulation fight)
        {
            if (this[aura].Active)
            {
                if (this[aura].Stacks < aura.MaxStacks)
                    ApplyEffects(aura, fight);

                this[aura].End.Timestamp = fight.Timestamp + aura.Duration;
            }

            else
            {
                ApplyEffects(aura, fight);

                this[aura].End = new AuraEndEvent(aura, fight, fight.Timestamp + aura.Duration);

                fight.Queue.Add(this[aura].End);
            }
        }

        private void ApplyEffects(Aura aura, FightSimulation fight)
        {
            foreach (AuraEffect effect in aura.Effects)
            {
                effect.Apply(aura, fight);
            }

            this[aura].Stacks++;
        }

        public void Cancel(Aura aura, FightSimulation fight)
        {
            for (int i = 0; i < this[aura].Stacks; i++)
            {
                foreach (AuraEffect effect in aura.Effects)                
                    effect.Remove(aura, fight);                
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