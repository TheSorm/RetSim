using RetSim.Items;
using RetSim.Units.Player.Static;
using RetSimDesktop.ViewModel;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SelectedGear : INotifyPropertyChanged
    {
        private EquippableItem selectedHead;
        private EquippableItem selectedNeck;
        private EquippableItem selectedShoulders;
        private EquippableItem selectedBack;
        private EquippableItem selectedChest;
        private EquippableItem selectedWrists;
        private EquippableItem selectedHands;
        private EquippableItem selectedWaist;
        private EquippableItem selectedLegs;
        private EquippableItem selectedFeet;
        private EquippableItem selectedFinger1;
        private EquippableItem selectedFinger2;
        private EquippableItem selectedTrinket1;
        private EquippableItem selectedTrinket2;
        private EquippableItem selectedRelic;
        private EquippableWeapon selectedWeapon;

        public EquippableItem SelectedHead
        {
            get { return selectedHead; }
            set
            {
                selectedHead = value;
                OnPropertyChanged(nameof(SelectedHead));
            }
        }

        public EquippableItem SelectedNeck
        {
            get { return selectedNeck; }
            set
            {
                selectedNeck = value;
                OnPropertyChanged(nameof(SelectedNeck));
            }
        }

        public EquippableItem SelectedShoulders
        {
            get { return selectedShoulders; }
            set
            {
                selectedShoulders = value;
                OnPropertyChanged(nameof(SelectedShoulders));
            }
        }

        public EquippableItem SelectedBack
        {
            get { return selectedBack; }
            set
            {
                selectedBack = value;
                OnPropertyChanged(nameof(SelectedBack));
            }
        }

        public EquippableItem SelectedChest
        {
            get { return selectedChest; }
            set
            {
                selectedChest = value;
                OnPropertyChanged(nameof(SelectedChest));
            }
        }

        public EquippableItem SelectedWrists
        {
            get { return selectedWrists; }
            set
            {
                selectedWrists = value;
                OnPropertyChanged(nameof(SelectedWrists));
            }
        }

        public EquippableItem SelectedHands
        {
            get { return selectedHands; }
            set
            {
                selectedHands = value;
                OnPropertyChanged(nameof(SelectedHands));
            }
        }

        public EquippableItem SelectedWaist
        {
            get { return selectedWaist; }
            set
            {
                selectedWaist = value;
                OnPropertyChanged(nameof(SelectedWaist));
            }
        }

        public EquippableItem SelectedLegs
        {
            get { return selectedLegs; }
            set
            {
                selectedLegs = value;
                OnPropertyChanged(nameof(SelectedLegs));
            }
        }

        public EquippableItem SelectedFeet
        {
            get { return selectedFeet; }
            set
            {
                selectedFeet = value;
                OnPropertyChanged(nameof(SelectedFeet));
            }
        }

        public EquippableItem SelectedFinger1
        {
            get { return selectedFinger1; }
            set
            {
                selectedFinger1 = value;
                OnPropertyChanged(nameof(SelectedFinger1));
            }
        }

        public EquippableItem SelectedFinger2
        {
            get { return selectedFinger2; }
            set
            {
                selectedFinger2 = value;
                OnPropertyChanged(nameof(SelectedFinger2));
            }
        }

        public EquippableItem SelectedTrinket1
        {
            get { return selectedTrinket1; }
            set
            {
                selectedTrinket1 = value;
                OnPropertyChanged(nameof(SelectedTrinket1));
            }
        }

        public EquippableItem SelectedTrinket2
        {
            get { return selectedTrinket2; }
            set
            {
                selectedTrinket2 = value;
                OnPropertyChanged(nameof(SelectedTrinket2));
            }
        }
        public EquippableItem SelectedRelic
        {
            get { return selectedRelic; }
            set
            {
                selectedRelic = value;
                OnPropertyChanged(nameof(SelectedRelic));
            }
        }

        public EquippableWeapon SelectedWeapon
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
                Head = retSimUIModel.SelectedGear.SelectedHead,
                Neck = retSimUIModel.SelectedGear.SelectedNeck,
                Shoulders = retSimUIModel.SelectedGear.SelectedShoulders,
                Back = retSimUIModel.SelectedGear.SelectedBack,
                Chest = retSimUIModel.SelectedGear.SelectedChest,
                Wrists = retSimUIModel.SelectedGear.SelectedWrists,
                Hands = retSimUIModel.SelectedGear.SelectedHands,
                Waist = retSimUIModel.SelectedGear.SelectedWaist,
                Legs = retSimUIModel.SelectedGear.SelectedLegs,
                Feet = retSimUIModel.SelectedGear.SelectedFeet,
                Finger1 = retSimUIModel.SelectedGear.SelectedFinger1,
                Finger2 = retSimUIModel.SelectedGear.SelectedFinger2,
                Trinket1 = retSimUIModel.SelectedGear.SelectedTrinket1,
                Trinket2 = retSimUIModel.SelectedGear.SelectedTrinket2,
                Relic = retSimUIModel.SelectedGear.SelectedRelic,
                Weapon = retSimUIModel.SelectedGear.SelectedWeapon,
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
