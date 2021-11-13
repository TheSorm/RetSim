using RetSim.Items;
using RetSim.Units.Player.Static;
using RetSimDesktop.ViewModel;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SelectedGear : INotifyPropertyChanged
    {
        private DisplayGear selectedHead;
        private DisplayGear selectedNeck;
        private DisplayGear selectedShoulders;
        private DisplayGear selectedBack;
        private DisplayGear selectedChest;
        private DisplayGear selectedWrists;
        private DisplayGear selectedHands;
        private DisplayGear selectedWaist;
        private DisplayGear selectedLegs;
        private DisplayGear selectedFeet;
        private DisplayGear selectedFinger1;
        private DisplayGear selectedFinger2;
        private DisplayGear selectedTrinket1;
        private DisplayGear selectedTrinket2;
        private DisplayGear selectedRelic;
        private DisplayWeapon selectedWeapon;

        private Enchant headEnchant;
        private Enchant shouldersEnchant;
        private Enchant backEnchant;
        private Enchant chestEnchant;
        private Enchant wristsEnchant;
        private Enchant handsEnchant;
        private Enchant legsEnchant;
        private Enchant feetEnchant;
        private Enchant finger1Enchant;
        private Enchant finger2Enchant;
        private Enchant weaponEnchant;

        public DisplayGear SelectedHead
        {
            get { return selectedHead; }
            set
            {
                selectedHead = value;
                OnPropertyChanged(nameof(SelectedHead));
            }
        }

        public DisplayGear SelectedNeck
        {
            get { return selectedNeck; }
            set
            {
                selectedNeck = value;
                OnPropertyChanged(nameof(SelectedNeck));
            }
        }

        public DisplayGear SelectedShoulders
        {
            get { return selectedShoulders; }
            set
            {
                selectedShoulders = value;
                OnPropertyChanged(nameof(SelectedShoulders));
            }
        }

        public DisplayGear SelectedBack
        {
            get { return selectedBack; }
            set
            {
                selectedBack = value;
                OnPropertyChanged(nameof(SelectedBack));
            }
        }

        public DisplayGear SelectedChest
        {
            get { return selectedChest; }
            set
            {
                selectedChest = value;
                OnPropertyChanged(nameof(SelectedChest));
            }
        }

        public DisplayGear SelectedWrists
        {
            get { return selectedWrists; }
            set
            {
                selectedWrists = value;
                OnPropertyChanged(nameof(SelectedWrists));
            }
        }

        public DisplayGear SelectedHands
        {
            get { return selectedHands; }
            set
            {
                selectedHands = value;
                OnPropertyChanged(nameof(SelectedHands));
            }
        }

        public DisplayGear SelectedWaist
        {
            get { return selectedWaist; }
            set
            {
                selectedWaist = value;
                OnPropertyChanged(nameof(SelectedWaist));
            }
        }

        public DisplayGear SelectedLegs
        {
            get { return selectedLegs; }
            set
            {
                selectedLegs = value;
                OnPropertyChanged(nameof(SelectedLegs));
            }
        }

        public DisplayGear SelectedFeet
        {
            get { return selectedFeet; }
            set
            {
                selectedFeet = value;
                OnPropertyChanged(nameof(SelectedFeet));
            }
        }

        public DisplayGear SelectedFinger1
        {
            get { return selectedFinger1; }
            set
            {
                selectedFinger1 = value;
                OnPropertyChanged(nameof(SelectedFinger1));
            }
        }

        public DisplayGear SelectedFinger2
        {
            get { return selectedFinger2; }
            set
            {
                selectedFinger2 = value;
                OnPropertyChanged(nameof(SelectedFinger2));
            }
        }

        public DisplayGear SelectedTrinket1
        {
            get { return selectedTrinket1; }
            set
            {
                selectedTrinket1 = value;
                OnPropertyChanged(nameof(SelectedTrinket1));
            }
        }

        public DisplayGear SelectedTrinket2
        {
            get { return selectedTrinket2; }
            set
            {
                selectedTrinket2 = value;
                OnPropertyChanged(nameof(SelectedTrinket2));
            }
        }
        public DisplayGear SelectedRelic
        {
            get { return selectedRelic; }
            set
            {
                selectedRelic = value;
                OnPropertyChanged(nameof(SelectedRelic));
            }
        }

        public DisplayWeapon SelectedWeapon 
        { 
            get { return selectedWeapon; } 
            set { 
                selectedWeapon = value; 
                OnPropertyChanged(nameof(SelectedWeapon)); 
            } 
        }

        public Enchant HeadEnchant { get { return headEnchant; } set { headEnchant = value; OnPropertyChanged(nameof(HeadEnchant)); } }
        public Enchant ShouldersEnchant { get { return shouldersEnchant; } set { shouldersEnchant = value; OnPropertyChanged(nameof(ShouldersEnchant)); } }
        public Enchant BackEnchant { get { return backEnchant; } set { backEnchant = value; OnPropertyChanged(nameof(BackEnchant)); } }
        public Enchant ChestEnchant { get { return chestEnchant; } set { chestEnchant = value; OnPropertyChanged(nameof(ChestEnchant)); } }
        public Enchant WristsEnchant { get { return wristsEnchant; } set { wristsEnchant = value; OnPropertyChanged(nameof(WristsEnchant)); } }
        public Enchant HandsEnchant { get { return handsEnchant; } set { handsEnchant = value; OnPropertyChanged(nameof(HandsEnchant)); } }
        public Enchant LegsEnchant { get { return legsEnchant; } set { legsEnchant = value; OnPropertyChanged(nameof(LegsEnchant)); } }
        public Enchant FeetEnchant { get { return feetEnchant; } set { feetEnchant = value; OnPropertyChanged(nameof(FeetEnchant)); } }
        public Enchant Finger1Enchant { get { return finger1Enchant; } set { finger1Enchant = value; OnPropertyChanged(nameof(Finger1Enchant)); } }
        public Enchant Finger2Enchant { get { return finger2Enchant; } set { finger2Enchant = value; OnPropertyChanged(nameof(Finger2Enchant)); } }
        public Enchant WeaponEnchant { get { return weaponEnchant; } set { weaponEnchant = value; OnPropertyChanged(nameof(WeaponEnchant)); } }


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
                HeadEnchant = retSimUIModel.SelectedGear.HeadEnchant,

                ShouldersEnchant = retSimUIModel.SelectedGear.ShouldersEnchant,
                BackEnchant = retSimUIModel.SelectedGear.BackEnchant,
                ChestEnchant = retSimUIModel.SelectedGear.ChestEnchant,
                WristsEnchant = retSimUIModel.SelectedGear.WristsEnchant,
                HandsEnchant = retSimUIModel.SelectedGear.HandsEnchant,
                LegsEnchant = retSimUIModel.SelectedGear.LegsEnchant,
                FeetEnchant = retSimUIModel.SelectedGear.FeetEnchant,
                Finger1Enchant = retSimUIModel.SelectedGear.Finger1Enchant,
                Finger2Enchant = retSimUIModel.SelectedGear.Finger2Enchant,
                WeaponEnchant = retSimUIModel.SelectedGear.WeaponEnchant,
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
