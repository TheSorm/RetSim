using System.Collections.Generic;

namespace RetSim
{
    public record Aura
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int Duration { get; init; }
        public int MaxStacks { get; init; }
        public List<AuraEffect> Effects { get; set; }
    }

    public record Seal : Aura
    {
        public int Persist { get; init; }

        public List<Seal> ExclusiveWith { get; set; }
    }
}