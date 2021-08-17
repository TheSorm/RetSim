
namespace RetSim
{
    public record Spell
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int Cooldown { get; init; }
    }
}
