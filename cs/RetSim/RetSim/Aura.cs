﻿using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim
{
    public class Aura
    {
        public Spell Parent { get; set; }
        public virtual int Duration { get; init; }
        public int MaxStacks { get; init; } = 1;
        public List<AuraEffect> Effects { get; set; } = null;

        public virtual void OnApply(FightSimulation fight)
        {
        }
    }

    public class Seal : Aura
    {
        public override int Duration { get; init; } = 30000;
        public int Persist { get; init; } = 0;
        public List<Seal> ExclusiveWith { get; set; }

        public override void OnApply(FightSimulation fight)
        {
            foreach (Seal other in ExclusiveWith)
            {
                if (fight.Player.Auras.IsActive(other))
                {
                    if (other.Persist == 0)
                        fight.Player.Auras[other].End.Timestamp = fight.Timestamp;

                    else
                        fight.Player.Auras[other].End.Timestamp = fight.Timestamp + other.Persist;
                }
            }
        }
    }
}