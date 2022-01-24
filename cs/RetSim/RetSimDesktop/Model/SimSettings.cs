using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SimSettings : INotifyPropertyChanged
    {
        private int simulationCount = 10000;
        private int maxCSDelay = 0;

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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
