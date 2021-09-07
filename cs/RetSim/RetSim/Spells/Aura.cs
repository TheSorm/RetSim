using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim
{
    public class Aura
    {
        public Spell Parent { get; set; }
        public virtual int Duration { get; init; } = 0;
        public int MaxStacks { get; init; } = 1;
        public List<AuraEffect> Effects { get; set; } = null;
    }

    public class Seal : Aura
    {
        public override int Duration { get; init; } = 30000;
        public int Persist { get; init; } = 0;
        public List<Seal> ExclusiveWith { get; set; }
        public Judgement Judgement { get; set; }
    }

    public enum AuraChangeType
    {
        Gain = 1,
        Fade = 2,
        Refresh = 3
    }
}