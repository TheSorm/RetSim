using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SimSettings : INotifyPropertyChanged
    {
        private int simulationCount = 10000;
        private int maxCSDelay = 0;
        private bool useExorcism = true;
        private bool useConsecration = true;

        public int SimulationCount
        {
            get { return simulationCount; }
            set
            {
                simulationCount = value;
                OnPropertyChanged(nameof(SimulationCount));
            }
        }

        public int MaxCSDelay
        {
            get { return maxCSDelay; }
            set
            {
                maxCSDelay = value;
                OnPropertyChanged(nameof(MaxCSDelay));
            }
        }

        public bool UseExorcism
        {
            get { return useExorcism; }
            set
            {
                useExorcism = value;
                OnPropertyChanged(nameof(UseExorcism));
            }
        }

        public bool UseConsecration
        {
            get { return useConsecration; }
            set
            {
                useConsecration = value;
                OnPropertyChanged(nameof(UseConsecration));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
