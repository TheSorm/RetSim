using RetSim.Simulation;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SimOutput : INotifyPropertyChanged
    {
        private int progress;
        private float dps;
        private float min;
        private float max;
        private CombatLog medianCombatLog;
        private CombatLog minCombatLog;
        private CombatLog maxCombatLog;

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
        public CombatLog MedianCombatLog
        {
            get { return medianCombatLog; }
            set
            {
                medianCombatLog = value;
                OnPropertyChanged(nameof(MedianCombatLog));
            }
        }

        public CombatLog MinCombatLog
        {
            get { return minCombatLog; }
            set
            {
                minCombatLog = value;
                OnPropertyChanged(nameof(MinCombatLog));
            }
        }

        public CombatLog MaxCombatLog
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
