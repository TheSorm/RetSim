using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class EncounterSettings : INotifyPropertyChanged
    {
        private int minFightDuration = 180000;
        private int maxFightDuration = 200000;
        private int encounterID = 0;

        public int EncounterID
        {
            get { return encounterID; }
            set
            {
                encounterID = value;
                OnPropertyChanged(nameof(EncounterID));
            }
        }

        public int MinFightDuration
        {
            get { return minFightDuration; }
            set
            {
                minFightDuration = value;
                OnPropertyChanged(nameof(MinFightDuration));
            }
        }

        public int MaxFightDuration
        {
            get { return maxFightDuration; }
            set
            {
                maxFightDuration = value;
                OnPropertyChanged(nameof(MaxFightDuration));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
