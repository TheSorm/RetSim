using System;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SimSettings : INotifyPropertyChanged
    {
        private string simulationCount;
        private string minFightDuration;
        private string maxFightDuration;

        public string SimulationCountSetting
        {
            get { return simulationCount; }
            set
            {
                simulationCount = value;
                OnPropertyChanged(nameof(SimulationCountSetting));
            }
        }
        public int SimulationCount
        {
            get { return Int32.Parse(simulationCount); }
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
