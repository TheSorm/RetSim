using RetSim.Items;
using RetSim.Units.Player.Static;
using RetSimDesktop.ViewModel;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SelectedGear : INotifyPropertyChanged
    {
        private ItemDPS selectedHead;
        private ItemDPS selectedNeck;
        private ItemDPS selectedShoulders;
        private ItemDPS selectedBack;
        private ItemDPS selectedChest;
        private ItemDPS selectedWrists;
        private ItemDPS selectedHands;
        private ItemDPS selectedWaist;
        private ItemDPS selectedLegs;
        private ItemDPS selectedFeet;
        private ItemDPS selectedFinger1;
        private ItemDPS selectedFinger2;
        private ItemDPS selectedTrinket1;
        private ItemDPS selectedTrinket2;
        private ItemDPS selectedRelic;
        private WeaponDPS selectedWeapon;

        public ItemDPS SelectedHead
        {
            get { return selectedHead; }
            set
            {
                selectedHead = value;
                OnPropertyChanged(nameof(SelectedHead));
            }
        }

        public ItemDPS SelectedNeck
        {
            get { return selectedNeck; }
            set
            {
                selectedNeck = value;
                OnPropertyChanged(nameof(SelectedNeck));
            }
        }

        public ItemDPS SelectedShoulders
        {
            get { return selectedShoulders; }
            set
            {
                selectedShoulders = value;
                OnPropertyChanged(nameof(SelectedShoulders));
            }
        }

        public ItemDPS SelectedBack
        {
            get { return selectedBack; }
            set
            {
                selectedBack = value;
                OnPropertyChanged(nameof(SelectedBack));
            }
        }

        public ItemDPS SelectedChest
        {
            get { return selectedChest; }
            set
            {
                selectedChest = value;
                OnPropertyChanged(nameof(SelectedChest));
            }
        }

        public ItemDPS SelectedWrists
        {
            get { return selectedWrists; }
            set
            {
                selectedWrists = value;
                OnPropertyChanged(nameof(SelectedWrists));
            }
        }

        public ItemDPS SelectedHands
        {
            get { return selectedHands; }
            set
            {
                selectedHands = value;
                OnPropertyChanged(nameof(SelectedHands));
            }
        }

        public ItemDPS SelectedWaist
        {
            get { return selectedWaist; }
            set
            {
                selectedWaist = value;
                OnPropertyChanged(nameof(SelectedWaist));
            }
        }

        public ItemDPS SelectedLegs
        {
            get { return selectedLegs; }
            set
            {
                selectedLegs = value;
                OnPropertyChanged(nameof(SelectedLegs));
            }
        }

        public ItemDPS SelectedFeet
        {
            get { return selectedFeet; }
            set
            {
                selectedFeet = value;
                OnPropertyChanged(nameof(SelectedFeet));
            }
        }

        public ItemDPS SelectedFinger1
        {
            get { return selectedFinger1; }
            set
            {
                selectedFinger1 = value;
                OnPropertyChanged(nameof(SelectedFinger1));
            }
        }

        public ItemDPS SelectedFinger2
        {
            get { return selectedFinger2; }
            set
            {
                selectedFinger2 = value;
                OnPropertyChanged(nameof(SelectedFinger2));
            }
        }

        public ItemDPS SelectedTrinket1
        {
            get { return selectedTrinket1; }
            set
            {
                selectedTrinket1 = value;
                OnPropertyChanged(nameof(SelectedTrinket1));
            }
        }

        public ItemDPS SelectedTrinket2
        {
            get { return selectedTrinket2; }
            set
            {
                selectedTrinket2 = value;
                OnPropertyChanged(nameof(SelectedTrinket2));
            }
        }
        public ItemDPS SelectedRelic
        {
            get { return selectedRelic; }
            set
            {
                selectedRelic = value;
                OnPropertyChanged(nameof(SelectedRelic));
            }
        }

        public WeaponDPS SelectedWeapon
        {
            get { return selectedWeapon; }
            set
            {
                selectedWeapon = value;
                OnPropertyChanged(nameof(SelectedWeapon));
            }
        }

        public static Equipment GetEquipment(RetSimUIModel retSimUIModel)
        {
            return new()
            {
                Head = retSimUIModel.SelectedGear.SelectedHead.Item,
                Neck = retSimUIModel.SelectedGear.SelectedNeck.Item,
                Shoulders = retSimUIModel.SelectedGear.SelectedShoulders.Item,
                Back = retSimUIModel.SelectedGear.SelectedBack.Item,
                Chest = retSimUIModel.SelectedGear.SelectedChest.Item,
                Wrists = retSimUIModel.SelectedGear.SelectedWrists.Item,
                Hands = retSimUIModel.SelectedGear.SelectedHands.Item,
                Waist = retSimUIModel.SelectedGear.SelectedWaist.Item,
                Legs = retSimUIModel.SelectedGear.SelectedLegs.Item,
                Feet = retSimUIModel.SelectedGear.SelectedFeet.Item,
                Finger1 = retSimUIModel.SelectedGear.SelectedFinger1.Item,
                Finger2 = retSimUIModel.SelectedGear.SelectedFinger2.Item,
                Trinket1 = retSimUIModel.SelectedGear.SelectedTrinket1.Item,
                Trinket2 = retSimUIModel.SelectedGear.SelectedTrinket2.Item,
                Relic = retSimUIModel.SelectedGear.SelectedRelic.Item,
                Weapon = retSimUIModel.SelectedGear.SelectedWeapon.Weapon,
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
