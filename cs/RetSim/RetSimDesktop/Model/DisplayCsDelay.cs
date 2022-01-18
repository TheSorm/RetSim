using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class DisplayCsDelay : INotifyPropertyChanged
    {
        private float delay;
        private bool enabledForCsDelay;
        private float dpsDelta;

        public float Delay
        {
            get { return delay; }
            set
            {
                delay = value;
                OnPropertyChanged(nameof(Delay));
            }
        }

        public bool EnabledForCsDelay
        {
            get { return enabledForCsDelay; }
            set
            {
                enabledForCsDelay = value;
                OnPropertyChanged(nameof(EnabledForCsDelay));
            }
        }

        public float DpsDelta
        {
            get { return dpsDelta; }
            set
            {
                dpsDelta = value;
                OnPropertyChanged(nameof(DpsDelta));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
