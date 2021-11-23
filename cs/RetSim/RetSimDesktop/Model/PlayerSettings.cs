using RetSim.Units.Player.Static;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class PlayerSettings : INotifyPropertyChanged
    {
        private Races selectedRace = Races.Human;

        public Races SelectedRace
        {
            get => selectedRace;
            set
            {
                selectedRace = value;
                OnPropertyChanged(nameof(SelectedRace));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
