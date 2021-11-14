using RetSim.Items;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class DisplayWeapon : INotifyPropertyChanged
    {
        private float dps;
        private bool enabledForGearSim;
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
        public bool EnabledForGearSim
        {
            get { return enabledForGearSim; }
            set
            {
                enabledForGearSim = value;
                OnPropertyChanged(nameof(EnabledForGearSim));
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
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
