using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class PlayerSettings : INotifyPropertyChanged
    {
        private Races selectedRace = Races.Human;
        private ShattrathFaction selectedShattrathFaction = ShattrathFaction.Aldor;

        public Races SelectedRace
        {
            get => selectedRace;
            set
            {
                selectedRace = value;
                OnPropertyChanged(nameof(SelectedRace));
            }
        }
        public ShattrathFaction SelectedShattrathFaction
        {
            get => selectedShattrathFaction;
            set
            {
                selectedShattrathFaction = value;
                OnPropertyChanged(nameof(SelectedShattrathFaction));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
