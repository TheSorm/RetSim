using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RetSimDesktop.Model
{
    public class EncounterSettings : INotifyPropertyChanged
    {
        private int minFightDurationSeconds = 160;
        private int maxFightDurationSeconds = 180;
        private int encounterID = 54;

        public int EncounterID
        {
            get { return encounterID; }
            set
            {
                encounterID = value;
                OnPropertyChanged(nameof(EncounterID));
            }
        }

        public int MinFightDurationSeconds
        {
            get { return minFightDurationSeconds; }
            set
            {
                minFightDurationSeconds = value;
                OnPropertyChanged(nameof(MinFightDurationSeconds));
            }
        }

        public int MaxFightDurationSeconds
        {
            get { return maxFightDurationSeconds; }
            set
            {
                maxFightDurationSeconds = value;
                OnPropertyChanged(nameof(MaxFightDurationSeconds));
            }
        }

        [JsonIgnore]
        public int MinFightDurationMilliseconds
        {
            get { return minFightDurationSeconds * 1000; }
        }

        [JsonIgnore]
        public int MaxFightDurationMilliseconds
        {
            get { return maxFightDurationSeconds * 1000; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
