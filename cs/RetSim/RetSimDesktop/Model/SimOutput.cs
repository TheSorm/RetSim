using RetSim.Simulation.CombatLogEntries;
using System.Collections.Generic;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SimOutput : INotifyPropertyChanged
    {
        private int progress;
        private float dps;
        private float min;
        private float max;
        private List<LogEntry> medianCombatLog;
        private List<LogEntry> minCombatLog;
        private List<LogEntry> maxCombatLog;

        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        public float DPS
        {
            get { return dps; }
            set
            {
                dps = value;
                OnPropertyChanged(nameof(DPS));
            }
        }

        public float Min
        {
            get { return min; }
            set
            {
                min = value;
                OnPropertyChanged(nameof(Min));
            }
        }
        public float Max
        {
            get { return max; }
            set
            {
                max = value;
                OnPropertyChanged(nameof(Max));
            }
        }
        public List<LogEntry> MedianCombatLog
        {
            get { return medianCombatLog; }
            set
            {
                medianCombatLog = value;
                OnPropertyChanged(nameof(MedianCombatLog));
            }
        }

        public List<LogEntry> MinCombatLog
        {
            get { return minCombatLog; }
            set
            {
                minCombatLog = value;
                OnPropertyChanged(nameof(MinCombatLog));
            }
        }

        public List<LogEntry> MaxCombatLog
        {
            get { return maxCombatLog; }
            set
            {
                maxCombatLog = value;
                OnPropertyChanged(nameof(MaxCombatLog));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
