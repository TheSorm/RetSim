using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class SimButtonStatus : INotifyPropertyChanged
    {
        private bool isSimButtonEnabled;

        public bool IsSimButtonEnabled
        {
            get { return isSimButtonEnabled; }
            set
            {
                isSimButtonEnabled = value;
                OnPropertyChanged(nameof(IsSimButtonEnabled));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
