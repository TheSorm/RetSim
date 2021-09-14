using System;
using RetSim.Items;
using RetSim.Misc;
using RetSim.Units.Player.Static;

namespace RetSimWeb
{
    public class AppState
    {
        public Equipment Equipment { get; } = new();

        public void ChangeItem(int slot, EquippableItem item)
        {
            switch (slot)
            {
                case Constants.EquipmentSlots.Head:
                    Equipment.Head = item;
                    break;
                case Constants.EquipmentSlots.Neck:
                    Equipment.Neck = item;
                    break;
                case Constants.EquipmentSlots.Shoulders:
                    Equipment.Shoulders = item;
                    break;
                case Constants.EquipmentSlots.Back:
                    Equipment.Back = item;
                    break;
                case Constants.EquipmentSlots.Chest:
                    Equipment.Chest = item;
                    break;
                case Constants.EquipmentSlots.Wrists:
                    Equipment.Wrists = item;
                    break;
                case Constants.EquipmentSlots.Hands:
                    Equipment.Hands = item;
                    break;
                case Constants.EquipmentSlots.Waist:
                    Equipment.Waist = item;
                    break;
                case Constants.EquipmentSlots.Legs:
                    Equipment.Legs = item;
                    break;
                case Constants.EquipmentSlots.Feet:
                    Equipment.Feet = item;
                    break;
                case Constants.EquipmentSlots.Finger1:
                    Equipment.Finger1 = item;
                    break;
                case Constants.EquipmentSlots.Finger2:
                    Equipment.Finger2 = item;
                    break;
                case Constants.EquipmentSlots.Trinket1:
                    Equipment.Trinket1 = item;
                    break;
                case Constants.EquipmentSlots.Trinket2:
                    Equipment.Trinket2 = item;
                    break;
                case Constants.EquipmentSlots.Relic:
                    Equipment.Relic = item;
                    break;
                default:
                    break;
            }
            NotifyStateChanged();
        }

        public void GemsUpdated()
        {
            NotifyStateChanged();
        }



        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
