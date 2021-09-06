using System;

namespace RetSim.Items
{
    public class EquippableArmor : EquippableItem
    {
        public int ArmorValue { get; init; }

        public static new EquippableArmor Get(int id, Gem[] gems)
        {
            return (EquippableArmor)EquippableItem.Get(id, gems);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
