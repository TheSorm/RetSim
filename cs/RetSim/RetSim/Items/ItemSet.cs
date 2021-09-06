using System.Collections.Generic;

namespace RetSim.Items
{
    public record ItemSet
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public List<SetSpell> SetSpells { get; init; }
    }

    public record SetSpell
    {
        public string Name { get; init; }
        public int ID { get; init; }
        public int RequiredCount { get; init; }

    }
}
