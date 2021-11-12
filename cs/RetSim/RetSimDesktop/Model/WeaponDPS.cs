using RetSim.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetSimDesktop.Model
{
    public class WeaponDPS : INotifyPropertyChanged
    {
        private float dps;
        private EquippableWeapon weapon;

        public float DPS
        {
            get { return dps; }
            set
            {
                dps = value;
                OnPropertyChanged(nameof(DPS));
            }
        }
        public EquippableWeapon Weapon
        {
            get { return weapon; }
            set
            {
                weapon = value;
                OnPropertyChanged(nameof(Weapon));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
