using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class TooltipSettings : INotifyPropertyChanged
    {
        private int hoverItemID;

        public int HoverItemID
        {
            get { return hoverItemID; }
            set
            {
                hoverItemID = value;
                OnPropertyChanged(nameof(HoverItemID));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
