using System;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SimSettings : INotifyPropertyChanged
    {
        private string simulationCount = "10000";


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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
