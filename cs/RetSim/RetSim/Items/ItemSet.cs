using System.Collections.Generic;

namespace RetSim.Items
{
    public record ItemSet
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public List<SetAura> SetAuras { get; init; }
    }

    public record SetAura
    {
        public string Name { get; init; }
        public int ID { get; init; }
        public int RequiredCount { get; init; }

    }
}
