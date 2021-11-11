using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetSimDesktop.Model
{
    public class SelectedPhases : INotifyPropertyChanged
    {
        private bool phase1Selected;
        private bool phase2Selected;
        private bool phase3Selected;
        private bool phase4Selected;
        private bool phase5Selected;

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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
