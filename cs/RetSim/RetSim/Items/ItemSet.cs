using System.Collections.Generic;

namespace RetSim.Items
{
    public record ItemSet
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public List<SetEffect> SetEffects { get; init; }
    }

    public record SetEffect
    {
        public string Name { get; init; }
        public int ID { get; init; }
        public int RequiredCount { get; init; }

    }
}
