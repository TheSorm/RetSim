using RetSim.Items;

namespace RetSim
{
    public record Equipment
    {
        public Stats Stats { get => CalculateStats(); }

        public EquippableArmor Head { get; init; }
        public EquippableArmor Neck { get; init; }
        public EquippableArmor Shoulder { get; init; }
        public EquippableArmor Cloak { get; init; }
        public EquippableArmor Chest { get; init; }
        public EquippableArmor Wrist { get; init; }
        public EquippableArmor Hand { get; init; }
        public EquippableArmor Waist { get; init; }
        public EquippableArmor Legs { get; init; }
        public EquippableArmor Feet { get; init; }
        public EquippableArmor Finger1 { get; init; }
        public EquippableArmor Finger2 { get; init; }
        public EquippableArmor Trinket1 { get; init; }
        public EquippableArmor Trinket2 { get; init; }
        public EquippableArmor Relic { get; init; }
        public EquippableWeapon Weapon { get; init; }

        private Stats CalculateStats()
        {
            //TODO Calculate Set Boni
            return Head.Stats + Neck.Stats + Shoulder.Stats + Cloak.Stats + Chest.Stats + Wrist.Stats + Hand.Stats + Waist.Stats
                + Legs.Stats + Feet.Stats + Finger1.Stats + Finger2.Stats + Trinket1.Stats + Trinket2.Stats + Relic.Stats + Weapon.Stats;
        }
    }
}
