using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SelectedPhases : INotifyPropertyChanged
    {
        private bool phase1Selected = true;
        private bool phase2Selected = true;
        private bool phase3Selected = true;
        private bool phase4Selected = false;
        private bool phase5Selected = false;

        public bool Phase1Selected
        {
            get { return phase1Selected; }
            set
            {
                phase1Selected = value;
                OnPropertyChanged(nameof(Phase1Selected));
            }
        }

        public bool Phase2Selected
        {
            get { return phase2Selected; }
            set
            {
                phase2Selected = value;
                OnPropertyChanged(nameof(Phase2Selected));
            }
        }

        public bool Phase3Selected
        {
            get { return phase3Selected; }
            set
            {
                phase3Selected = value;
                OnPropertyChanged(nameof(Phase3Selected));
            }
        }

        public bool Phase4Selected
        {
            get { return phase4Selected; }
            set
            {
                phase4Selected = value;
                OnPropertyChanged(nameof(Phase4Selected));
            }
        }

        public bool Phase5Selected
        {
            get { return phase5Selected; }
            set
            {
                phase5Selected = value;
                OnPropertyChanged(nameof(Phase5Selected));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
