
namespace RetSim
{
    internal record Spell
    {
        public int SpellId { get; init; }
        public string Name { get; init; }
        public int Cooldown { get; init; }
    }
}
