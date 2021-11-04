using RetSim.Items;
using RetSimDesktop.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static RetSim.Data.Items;

namespace RetSimDesktop.ViewModel
{
    public class RetSimUIModel
    {
        private SelectedGear _SelectedGear;
        private SimOutput _CurrentSimOutput;

        public RetSimUIModel()
        {
            Gem strength = Gems[24027];
            Gem crit = Gems[24058];
            Gem stamina = Gems[24054];

            _SelectedGear = new SelectedGear
            {
                SelectedHead = EquippableItem.GetItemWithGems(29073, new Gem[] { MetaGems[32409], strength }),
                SelectedNeck = EquippableItem.GetItemWithGems(29381, null),
                SelectedShoulders = EquippableItem.GetItemWithGems(29075, new Gem[] { strength, crit }),
                SelectedBack = EquippableItem.GetItemWithGems(24259, new Gem[] { strength }),
                SelectedChest = EquippableItem.GetItemWithGems(29071, new Gem[] { strength, strength, strength }),
                SelectedWrists = EquippableItem.GetItemWithGems(28795, new Gem[] { strength, stamina }),
                SelectedHands = EquippableItem.GetItemWithGems(30644, null),
                SelectedWaist = EquippableItem.GetItemWithGems(28779, new Gem[] { strength, stamina }),
                SelectedLegs = EquippableItem.GetItemWithGems(31544, null),
                SelectedFeet = EquippableItem.GetItemWithGems(28608, new Gem[] { strength, crit }),
                SelectedFinger1 = EquippableItem.GetItemWithGems(30834, null),
                SelectedFinger2 = EquippableItem.GetItemWithGems(28757, null),
                SelectedTrinket1 = EquippableItem.GetItemWithGems(29383, null),
                SelectedTrinket2 = EquippableItem.GetItemWithGems(28830, null),
                SelectedRelic = EquippableItem.GetItemWithGems(27484, null),
                SelectedWeapon = Weapons[28429],
            };

            _CurrentSimOutput = new SimOutput() { Progress = 0, DPS = 0 };
        }

        public SelectedGear SelectedGear
        {
            get { return _SelectedGear; }
            set { _SelectedGear = value; }
        }

        public SimOutput CurrentSimOutput
        {
            get { return _CurrentSimOutput; }
            set { _CurrentSimOutput = value; }
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            #region ICommand Members  

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }

            #endregion
        }
    }
}
