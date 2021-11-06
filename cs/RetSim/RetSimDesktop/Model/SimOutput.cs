using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SimOutput : INotifyPropertyChanged
    {
        private int progress;
        private float dps;

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
