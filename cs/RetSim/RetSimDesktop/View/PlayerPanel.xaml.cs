using RetSim.Data;
using RetSim.Items;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for PlayerPanel.xaml
    /// </summary>
    public partial class PlayerPanel : UserControl
    {

        private Dictionary<Image, int> ImageToSlot = new();
        public PlayerPanel()
        {
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.PlayerSettings.PropertyChanged += PlayerSettings_PropertyChanged;

                    PlayerSettings_PropertyChanged(this, new PropertyChangedEventArgs(""));
                }
            };
            InitializeComponent();
        }

        private void PlayerSettings_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                StatPanelBoxHeader.Content = "Level 70 " + Collections.Races[retSimUIModel.PlayerSettings.SelectedRace.ToString()].Name + " Paladin";
            }
        }

        private void PlayerBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.TooltipSettings.OverlayControl = PlayerPanelControl;
            }
        }

        private void PlayerBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.TooltipSettings.OverlayControl = null;
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel && sender is Image image && image.GetBindingExpression(Image.SourceProperty).ResolvedSource is EquippableItem item)
            {
                retSimUIModel.TooltipSettings.HoverItemID = item.ID;
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.TooltipSettings.HoverItemID = 0;
            }
        }

        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow window && sender is Image image)
            {
                window.SwitchToGearSelection(ImageToSlot[image]);
            }
        }
    }
}
