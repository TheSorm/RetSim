namespace RetSim.Units.Enemy
{
    public class Boss
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public CreatureType CreatureType { get; init; }
        public ArmorCategory ArmorCategory { get; init; }
        public string Raid { get; init; }

        public override string ToString()
        {
            return $"Name: {Name} - ID: {ID} / Type: {CreatureType} / Armor: {ArmorCategory} / Raid: {Raid}";
        }
    }
}