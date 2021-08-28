namespace RetSim.Items
{
    public class EquippableArmor : EquippableItem
    {
        public int ArmorValue { get; init; }
        public EquippableArmor(WowItemData.Armor data) : base(data)
        {
            ArmorValue = data.ArmorValue;
        }
    }
}
