using System;
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

        public int MinFightDurationSetting
        {
            get { return minFightDuration; }
            set
            {
                minFightDuration = value;
                OnPropertyChanged(nameof(MinFightDurationSetting));
            }
        }

        public int MinFightDuration
        {
            get { return minFightDuration; }
        }

        public int MaxFightDurationSetting
        {
            get { return maxFightDuration; }
            set
            {
                maxFightDuration = value;
                OnPropertyChanged(nameof(MaxFightDurationSetting));
            }
        }

        public int MaxFightDuration
        {
            get { return maxFightDuration; }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
