using RetSim.Items;
using RetSim.Units.UnitStats;
using RetSimDesktop.Model;
using RetSimDesktop.View;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearSlotSelect.xaml
    /// </summary>
    public partial class GearSlotSelect : UserControl
    {
        private static GearSim gearSimWorker = new();

        public int SlotID { get; set; }
        public IEnumerable<DisplayGear> SlotList
        {
            get => (IEnumerable<DisplayGear>)GetValue(SlotListProperty);
            set => SetValue(SlotListProperty, value);
        }

        public static readonly DependencyProperty SlotListProperty = DependencyProperty.Register(
            "SlotList",
            typeof(IEnumerable<DisplayGear>),
            typeof(GearSlotSelect));

        public List<Enchant> EnchantList
        {
            get => (List<Enchant>)GetValue(EnchantListProperty);
            set => SetValue(EnchantListProperty, value);
        }

        public static readonly DependencyProperty EnchantListProperty = DependencyProperty.Register(
            "EnchantList",
            typeof(List<Enchant>),
            typeof(GearSlotSelect));

        public DisplayGear SelectedItem
        {
            get => (DisplayGear)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(DisplayGear),
            typeof(GearSlotSelect));

        public Enchant SelectedEnchant
        {
            get => (Enchant)GetValue(SelectedEnchantProperty);
            set => SetValue(SelectedEnchantProperty, value);
        }

        public static readonly DependencyProperty SelectedEnchantProperty = DependencyProperty.Register(
            "SelectedEnchant",
            typeof(Enchant),
            typeof(GearSlotSelect));


        public GearSlotSelect()
        {
            InitializeComponent();
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    if (EnchantList == null)
                    {
                        EnchantComboBox.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        EnchantComboBox.Visibility = Visibility.Visible;
                    }
                }
            };

            gearSlot.SetBinding(DataGrid.ItemsSourceProperty, new Binding("SlotList")
            {
                Source = this,
                Mode = BindingMode.OneWay,
            });

            gearSlot.SetBinding(DataGrid.SelectedItemProperty, new Binding("SelectedItem")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            });

            EnchantComboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding("EnchantList")
            {
                Source = this,
                Mode = BindingMode.OneWay,
            });

            EnchantComboBox.SetBinding(ComboBox.SelectedItemProperty, new Binding("SelectedEnchant")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            });

            StatConverter statConverter = new();

            Binding strBinding = new("Item.Stats[" + StatName.Strength + "]");
            strBinding.Converter = statConverter;
            StrColumn.Binding = strBinding;
            Binding apBinding = new("Item.Stats[" + StatName.AttackPower + "]");
            apBinding.Converter = statConverter;
            APColumn.Binding = apBinding;
            Binding agiBinding = new("Item.Stats[" + StatName.Agility + "]");
            agiBinding.Converter = statConverter;
            AgiColumn.Binding = agiBinding;
            Binding critBinding = new("Item.Stats[" + StatName.CritRating + "]");
            critBinding.Converter = statConverter;
            CritColumn.Binding = critBinding;
            Binding hitBinding = new("Item.Stats[" + StatName.HitRating + "]");
            hitBinding.Converter = statConverter;
            HitColumn.Binding = hitBinding;
            Binding hasteBinding = new("Item.Stats[" + StatName.HasteRating + "]");
            hasteBinding.Converter = statConverter;
            HasteColumn.Binding = hasteBinding;
            Binding expBinding = new("Item.Stats[" + StatName.ExpertiseRating + "]");
            expBinding.Converter = statConverter;
            ExpColumn.Binding = expBinding;
            Binding apenBinding = new("Item.Stats[" + StatName.ArmorPenetration + "]");
            apenBinding.Converter = statConverter;
            APenColumn.Binding = apenBinding;
            Binding staBinding = new("Item.Stats[" + StatName.Stamina + "]");
            staBinding.Converter = statConverter;
            StaColumn.Binding = staBinding;
            Binding intBinding = new("Item.Stats[" + StatName.Intellect + "]");
            intBinding.Converter = statConverter;
            IntColumn.Binding = intBinding;
            Binding mp5Binding = new("Item.Stats[" + StatName.ManaPer5 + "]");
            mp5Binding.Converter = statConverter;
            MP5Column.Binding = mp5Binding;
            Binding spBinding = new("Item.Stats[" + StatName.SpellPower + "]");
            spBinding.Converter = statConverter;
            SPColumn.Binding = spBinding;
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGridCellTarget = (DataGridCell)sender;
            var header = dataGridCellTarget.Column.Header.ToString();

            Socket? selectedSocket = null;
            if (header == "Socket 1")
            {
                selectedSocket = SelectedItem.Item.Socket1;
            }
            else if (header == "Socket 2")
            {
                selectedSocket = SelectedItem.Item.Socket2;
            }
            else if (header == "Socket 3")
            {
                selectedSocket = SelectedItem.Item.Socket3;
            }

            if (selectedSocket != null)
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    GemPickerWindow gemPicker;
                    if (selectedSocket.Color == SocketColor.Meta)
                    {
                        gemPicker = new(RetSim.Data.Items.MetaGems.Values, selectedSocket.SocketedGem);
                    }
                    else
                    {
                        gemPicker = new(RetSim.Data.Items.Gems.Values, selectedSocket.SocketedGem);
                    }

                    retSimUIModel.TooltipSettings.HoverItemID = 0;
                    if (gemPicker.ShowDialog() == true)
                    {
                        selectedSocket.SocketedGem = gemPicker.SelectedGem;

                        retSimUIModel.SelectedGear.OnPropertyChanged("");
                        SelectedItem.OnPropertyChanged("");
                    }
                }
            }
        }

        private void GearSim_Click(object sender, RoutedEventArgs e)
        {
            if (!gearSimWorker.IsBusy && DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.SimButtonStatus.IsSimButtonEnabled = false;
                gearSimWorker.RunWorkerAsync(new Tuple<RetSimUIModel, IEnumerable<DisplayGear>, int>(retSimUIModel, SlotList, SlotID));
            }
        }

        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridCell cell)
            {
                if (DataContext is RetSimUIModel retSimUIModel && DataGridRow.GetRowContainingElement(cell).Item is DisplayGear displayItem)
                {
                    if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
                    {
                        System.Diagnostics.Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://tbc.wowhead.com/item=" + displayItem.Item.ID,
                            UseShellExecute = true
                        });
                    }
                    else if (e.ChangedButton == MouseButton.Right && e.ButtonState == MouseButtonState.Pressed)
                    {
                        var header = cell.Column.Header.ToString();
                        if (header == "Socket 1" && displayItem.Item.Socket1 != null)
                        {
                            displayItem.Item.Socket1.SocketedGem = null;
                        }
                        else if (header == "Socket 2" && displayItem.Item.Socket2 != null)
                        {
                            displayItem.Item.Socket2.SocketedGem = null;
                        }
                        else if (header == "Socket 3" && displayItem.Item.Socket3 != null)
                        {
                            displayItem.Item.Socket3.SocketedGem = null;
                        }
                        displayItem.OnPropertyChanged("");
                        DataGridCell_MouseEnter(cell, null);
                    }
                }
            }
        }

        private void ChkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            if (SlotList != null)
            {
                foreach (var displayItem in SlotList)
                {
                    displayItem.EnabledForGearSim = true;
                }
            }
        }

        private void ChkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            if (SlotList != null)
            {
                foreach (var displayItem in SlotList)
                {
                    displayItem.EnabledForGearSim = false;
                }
            }
        }
        private void DataGridCell_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is DataGridCell cell)
            {
                if (DataContext is RetSimUIModel retSimUIModel && DataGridRow.GetRowContainingElement(cell).Item is DisplayGear displayItem)
                {
                    var header = cell.Column.Header.ToString();

                    if (header == "Socket 1" && displayItem.Item.Socket1 != null && displayItem.Item.Socket1.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.Socket1.SocketedGem.ID;
                    }
                    else if (header == "Socket 2" && displayItem.Item.Socket2 != null && displayItem.Item.Socket2.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.Socket2.SocketedGem.ID;
                    }
                    else if (header == "Socket 3" && displayItem.Item.Socket3 != null && displayItem.Item.Socket3.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.Socket3.SocketedGem.ID;
                    }
                    else if (retSimUIModel.TooltipSettings.HoverItemID != displayItem.Item.ID)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.ID;
                    }
                }
            }
        }

        private void DataGridCell_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.TooltipSettings.HoverItemID = 0;
            }
        }
    }
    public class QualityToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var input = (Quality)value;
            return input switch
            {
                Quality.Poor => new SolidColorBrush(Color.FromRgb(157, 157, 157)),
                Quality.Common => new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Quality.Uncommon => new SolidColorBrush(Color.FromRgb(30, 255, 0)),
                Quality.Rare => new SolidColorBrush(Color.FromRgb(0, 112, 221)),
                Quality.Epic => new SolidColorBrush(Color.FromRgb(163, 53, 238)),
                Quality.Legendary => new SolidColorBrush(Color.FromRgb(255, 128, 0)),
                Quality.Artifact => new SolidColorBrush(Color.FromRgb(230, 204, 128)),
                _ => Brushes.Red,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class SocketBonusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is EquippableItem item && item.Socket1 != null)
            {
                return item.IsSocketBonusActive() ? "✓" : "✗";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class StatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((float)value).ToString("0;;' '");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class SocketConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not Socket socket)
            {
                return "-";
            }

            if (socket.SocketedGem != null)
            {
                return "[" + socket.Color.ToString() + "]";
            }

            return socket.Color.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
