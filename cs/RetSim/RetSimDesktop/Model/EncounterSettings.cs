using RetSim.Units.Enemy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetSim.Data;

namespace RetSimDesktop.Model
{
    public class EncounterSettings : INotifyPropertyChanged
    {
        private string minFightDuration = "180000";
        private string maxFightDuration = "200000";
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

        public string MinFightDurationSetting
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
            get { return Int32.Parse(minFightDuration); }
        }

        public string MaxFightDurationSetting
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
            get { return Int32.Parse(maxFightDuration); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
