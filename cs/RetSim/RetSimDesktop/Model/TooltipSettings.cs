using RetSim.Items;
using System.ComponentModel;
using System.Windows.Controls;

namespace RetSimDesktop.Model
{
    public class TooltipSettings : INotifyPropertyChanged
    {
        private int hoverItemID;
        private Control? overlayControl;
        private Enchant? ringEnchant;

        public int HoverItemID
        {
            get { return hoverItemID; }
            set
            {
                hoverItemID = value;
                OnPropertyChanged(nameof(HoverItemID));
            }
        }

        public Control? OverlayControl
        {
            get { return overlayControl; }
            set
            {
                overlayControl = value;
                OnPropertyChanged(nameof(OverlayControl));
            }
        }

        public Enchant? RingEnchant
        {
            get { return ringEnchant; }
            set
            {
                ringEnchant = value;
                OnPropertyChanged(nameof(RingEnchant));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
