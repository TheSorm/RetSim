
namespace RetSim
{
    internal record Spell
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int Cooldown { get; init; }
    }
}
