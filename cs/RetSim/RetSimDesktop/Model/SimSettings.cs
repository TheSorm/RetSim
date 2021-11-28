using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SimSettings : INotifyPropertyChanged
    {
        private int simulationCount = 10000;


        public int SimulationCount
        {
            get { return simulationCount; }
            set
            {
                simulationCount = value;
                OnPropertyChanged(nameof(SimulationCount));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
