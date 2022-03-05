using RetSim.Items;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearImage.xaml
    /// </summary>
    public partial class GearImage : UserControl
    {
        public DisplayGear Gear
        {
            get => (DisplayGear)GetValue(GearProperty);
            set => SetValue(GearProperty, value);
        }

        public static readonly DependencyProperty GearProperty = DependencyProperty.Register(
            "Gear",
            typeof(DisplayGear),
            typeof(GearImage),
            new PropertyMetadata(new PropertyChangedCallback(Gear_PropertyChanged)));

        public int SlotId
        {
            get => (int)GetValue(SlotIdProperty);
            set => SetValue(SlotIdProperty, value);
        }

        public static readonly DependencyProperty SlotIdProperty = DependencyProperty.Register(
            "SlotId",
            typeof(int),
            typeof(GearImage),
            new PropertyMetadata(new PropertyChangedCallback(Gear_PropertyChanged)));


        public GearImage()
        {
            InitializeComponent();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel && Gear != null)
            {
                if (SlotId == 10)
                {
                    retSimUIModel.TooltipSettings.RingEnchant = retSimUIModel.SelectedGear.Finger1Enchant;
                }
                else if (SlotId == 11)
                {
                    retSimUIModel.TooltipSettings.RingEnchant = retSimUIModel.SelectedGear.Finger2Enchant;
                }
                retSimUIModel.TooltipSettings.HoverItemID = Gear.Item.ID;
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.TooltipSettings.HoverItemID = 0;
                retSimUIModel.TooltipSettings.RingEnchant = null;
            }
        }

        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed && Window.GetWindow(this) is MainWindow window)
            {
                window.SwitchToGearSelection(SlotId);
            }
            else if (e.ChangedButton == MouseButton.Right && e.ButtonState == MouseButtonState.Pressed && DataContext is RetSimUIModel retSimUIModel)
            {
                if (SlotId == 0) retSimUIModel.SelectedGear.SelectedHead = null;
                else if (SlotId == 1) retSimUIModel.SelectedGear.SelectedNeck = null;
                else if (SlotId == 2) retSimUIModel.SelectedGear.SelectedShoulders = null;
                else if (SlotId == 3) retSimUIModel.SelectedGear.SelectedBack = null;
                else if (SlotId == 4) retSimUIModel.SelectedGear.SelectedChest = null;
                else if (SlotId == 5) retSimUIModel.SelectedGear.SelectedWrists = null;
                else if (SlotId == 6) retSimUIModel.SelectedGear.SelectedHands = null;
                else if (SlotId == 7) retSimUIModel.SelectedGear.SelectedWaist = null;
                else if (SlotId == 8) retSimUIModel.SelectedGear.SelectedLegs = null;
                else if (SlotId == 9) retSimUIModel.SelectedGear.SelectedFeet = null;
                else if (SlotId == 10) retSimUIModel.SelectedGear.SelectedFinger1 = null;
                else if (SlotId == 11) retSimUIModel.SelectedGear.SelectedFinger2 = null;
                else if (SlotId == 12) retSimUIModel.SelectedGear.SelectedTrinket1 = null;
                else if (SlotId == 13) retSimUIModel.SelectedGear.SelectedTrinket2 = null;
                else if (SlotId == 14) retSimUIModel.SelectedGear.SelectedRelic = null;
                else if (SlotId == 15) retSimUIModel.SelectedGear.SelectedWeapon = null;

                retSimUIModel.SelectedGear.OnPropertyChanged("");
            }
            else if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                if (Gear.Item != null)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://tbc.wowhead.com/item=" + Gear.Item.ID,
                        UseShellExecute = true
                    });
                }
            }
        }

        private void Socket_MouseEnter(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel && sender is Image image)
            {
                switch (image.GetBindingExpression(Image.SourceProperty).ResolvedSourcePropertyName)
                {
                    case "Socket1":
                        if (Gear.Item.Socket1.SocketedGem != null)
                            retSimUIModel.TooltipSettings.HoverItemID = Gear.Item.Socket1.SocketedGem.ID;
                        break;
                    case "Socket2":
                        if (Gear.Item.Socket2.SocketedGem != null)
                            retSimUIModel.TooltipSettings.HoverItemID = Gear.Item.Socket2.SocketedGem.ID;
                        break;
                    case "Socket3":
                        if (Gear.Item.Socket3.SocketedGem != null)
                            retSimUIModel.TooltipSettings.HoverItemID = Gear.Item.Socket3.SocketedGem.ID;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Socket_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.TooltipSettings.HoverItemID = 0;
            }
        }

        private void Socket_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel && sender is Image image)
            {
                Socket selectedSocket = null;
                switch (image.GetBindingExpression(Image.SourceProperty).ResolvedSourcePropertyName)
                {
                    case "Socket1":
                        selectedSocket = Gear.Item.Socket1;
                        break;
                    case "Socket2":
                        selectedSocket = Gear.Item.Socket2;
                        break;
                    case "Socket3":
                        selectedSocket = Gear.Item.Socket3;
                        break;
                    default:
                        break;
                }

                if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
                {
                    if (selectedSocket != null)
                    {
                        GemPickerWindow gemPicker;
                        if (selectedSocket.Color == SocketColor.Meta)
                        {
                            gemPicker = new(RetSim.Data.Items.MetaGems.Values, selectedSocket.SocketedGem, true);
                        }
                        else
                        {
                            gemPicker = new(RetSim.Data.Items.GemsSorted, selectedSocket.SocketedGem);
                        }

                        retSimUIModel.TooltipSettings.HoverItemID = 0;
                        if (gemPicker.ShowDialog() == true)
                        {
                            selectedSocket.SocketedGem = gemPicker.SelectedGem;

                            retSimUIModel.SelectedGear.OnPropertyChanged("");
                            retSimUIModel.AllGear[Gear.Item.ID].OnPropertyChanged("");
                            Socket1Image.GetBindingExpression(Image.SourceProperty).UpdateTarget();
                            Socket2Image.GetBindingExpression(Image.SourceProperty).UpdateTarget();
                            Socket3Image.GetBindingExpression(Image.SourceProperty).UpdateTarget();
                        }
                    }
                }
                else if (e.ChangedButton == MouseButton.Right && e.ButtonState == MouseButtonState.Pressed)
                {
                    if (selectedSocket != null)
                    {
                        selectedSocket.SocketedGem = null;
                        retSimUIModel.SelectedGear.OnPropertyChanged("");
                        retSimUIModel.AllGear[Gear.Item.ID].OnPropertyChanged("");
                        Socket1Image.GetBindingExpression(Image.SourceProperty).UpdateTarget();
                        Socket2Image.GetBindingExpression(Image.SourceProperty).UpdateTarget();
                        Socket3Image.GetBindingExpression(Image.SourceProperty).UpdateTarget();
                    }
                }
                else if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
                {
                    if (selectedSocket != null && selectedSocket.SocketedGem != null)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://tbc.wowhead.com/item=" + selectedSocket.SocketedGem.ID,
                            UseShellExecute = true
                        });
                    }
                }
            }
        }

        private static void Gear_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GearImage gearImage)
            {
                gearImage.ItemImage.GetBindingExpression(Image.SourceProperty).UpdateTarget();
            }
        }
    }

    public class ItemToImageConverterWithDefault : IValueConverter
    {
        private static readonly Dictionary<int, string> SlotToDefaultImage = new()
        {
            { 7, "inv_belt.jpg" },
            { 9, "inv_boots.jpg" },
            { 5, "inv_bracer.jpg" },
            { 6, "inv_gauntlets.jpg" },
            { 0, "inv_helmet.jpg" },
            { 1, "inv_jewelry_necklace.jpg" },
            { 10, "inv_jewelry_ring.jpg" },
            { 11, "inv_jewelry_ring.jpg" },
            { 3, "inv_misc_cape.jpg" },
            { 8, "inv_pants.jpg" },
            { 14, "inv_relics.jpg" },
            { 12, "inv_trinket.jpg" },
            { 13, "inv_trinket.jpg" },
            { 15, "inv_weapon.jpg" },
            { 2, "inv_shoulder.jpg" },
            { 4, "inv_chest.jpg" },
        };
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is GearImage gearImage)
            {
                if (gearImage.Gear != null && gearImage.Gear.Item != null && MediaMetaData.ItemsMetaData.ContainsKey(gearImage.Gear.Item.ID))
                {
                    return new BitmapImage(new Uri($"pack://application:,,,/Properties/Icons/{MediaMetaData.ItemsMetaData[gearImage.Gear.Item.ID].IconFileName}"));
                }
                return new BitmapImage(new Uri($"pack://application:,,,/Properties/Icons/{SlotToDefaultImage[gearImage.SlotId]}"));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class SocketToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Socket socket)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
