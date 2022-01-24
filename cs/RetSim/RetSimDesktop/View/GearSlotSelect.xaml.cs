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
using System.Windows.Media.Imaging;

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
                if (DataContext is RetSimUIModel retSimUIModel && DataGridRow.GetRowContainingElement(cell).Item is DisplayGear displayGear)
                {
                    if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://tbc.wowhead.com/item=" + displayGear.Item.ID,
                            UseShellExecute = true
                        });
                    }
                    else if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
                    {
                        var column = cell.Column;
                        if (cell.Column.Header.GetType() == typeof(CheckBox) && cell.Content is CheckBox checkBox)
                        {
                            checkBox.IsChecked = !checkBox.IsChecked;
                            e.Handled = true;
                        }
                        else if (column != null && column == Socket1Column || column == Socket2Column || column == Socket3Column)
                        {
                            Socket? selectedSocket = null;

                            if (column == Socket1Column)
                            {
                                selectedSocket = displayGear.Item.Socket1;
                            }
                            else if (column == Socket2Column)
                            {
                                selectedSocket = displayGear.Item.Socket2;
                            }
                            else if (column == Socket3Column)
                            {
                                selectedSocket = displayGear.Item.Socket3;
                            }

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
                                    displayGear.OnPropertyChanged("");
                                }
                                e.Handled = true;
                            }
                        }
                    }
                    else if (e.ChangedButton == MouseButton.Right && e.ButtonState == MouseButtonState.Pressed)
                    {
                        var column = cell.Column;
                        if (column != null && column == Socket1Column || column == Socket2Column || column == Socket3Column)
                        {
                            bool socketNotNull = false;
                            if (column == Socket1Column && displayGear.Item.Socket1 != null)
                            {
                                displayGear.Item.Socket1.SocketedGem = null;
                                socketNotNull = true;
                            }
                            else if (column == Socket2Column && displayGear.Item.Socket2 != null)
                            {
                                displayGear.Item.Socket2.SocketedGem = null;
                                socketNotNull = true;
                            }
                            else if (column == Socket3Column && displayGear.Item.Socket3 != null)
                            {
                                displayGear.Item.Socket3.SocketedGem = null;
                                socketNotNull = true;
                            }
                            if (socketNotNull)
                            {
                                displayGear.OnPropertyChanged("");
                                retSimUIModel.SelectedGear.OnPropertyChanged("");
                                DataGridCell_MouseEnter(cell, null);
                                return;
                            }
                        }
                        if (gearSlot.SelectedItem == displayGear)
                        {
                            gearSlot.SelectedItem = null;
                            retSimUIModel.SelectedGear.OnPropertyChanged("");
                        }
                    }
                }
            }
        }

        private void DataGridCell_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right && e.ButtonState == MouseButtonState.Released)
            {
                e.Handled = true;
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
                    var column = cell.Column;

                    if (column == Socket1Column && displayItem.Item.Socket1 != null && displayItem.Item.Socket1.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.Socket1.SocketedGem.ID;
                    }
                    else if (column == Socket2Column && displayItem.Item.Socket2 != null && displayItem.Item.Socket2.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.Socket2.SocketedGem.ID;
                    }
                    else if (column == Socket3Column && displayItem.Item.Socket3 != null && displayItem.Item.Socket3.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.Socket3.SocketedGem.ID;
                    }
                    else if (retSimUIModel.TooltipSettings.HoverItemID != displayItem.Item.ID)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.ID;
                    }

                    foreach (var item in SlotList)
                    {
                        if (item.Item.Slot == Slot.Finger)
                        {
                            retSimUIModel.TooltipSettings.RingEnchant = SelectedEnchant;
                        }
                        break;
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
        private static readonly Dictionary<StatName, string> StatToShortString = new()
        {
            { StatName.Stamina, "Stam" },
            { StatName.Intellect, "Int" },
            { StatName.ManaPer5, "MP5" },
            { StatName.Strength, "Str" },
            { StatName.AttackPower, "AP" },
            { StatName.Agility, "Agi " },
            { StatName.CritRating, "Crit" },
            { StatName.HitRating, "Hit" },
            { StatName.Haste, "Haste" },
            { StatName.ArmorPenetration, "APen" },
            { StatName.SpellPower, "SP" },
            { StatName.Resilience, "Resi" }
        };
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is EquippableItem item && item.Socket1 != null && item.SocketBonus != null)
            {
                foreach (var stat in item.SocketBonus.Keys)
                {
                    if (StatToShortString.ContainsKey(stat))
                        return $"+{item.SocketBonus[stat]} {StatToShortString[stat]}";
                }
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class SocketBonusActiveConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is EquippableItem item && item.Socket1 != null && item.IsSocketBonusActive())
            {
                return new SolidColorBrush(Colors.Black);
            }
            return new SolidColorBrush(Colors.LightGray);
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

    public class SocketToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Socket socket)
            {
                if (socket.SocketedGem != null && MediaMetaData.GemsToIconName.ContainsKey(socket.SocketedGem.ID))
                {
                    return new BitmapImage(new Uri($"pack://application:,,,/Properties/Icons/{MediaMetaData.GemsToIconName[socket.SocketedGem.ID]}"));
                }
                else if (socket.Color == SocketColor.Red)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Properties/Icons/red_socket.jpg"));
                }
                else if (socket.Color == SocketColor.Blue)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Properties/Icons/blue_socket.jpg"));
                }
                else if (socket.Color == SocketColor.Yellow)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Properties/Icons/yellow_socket.jpg"));
                }
                else if (socket.Color == SocketColor.Meta)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Properties/Icons/meta_socket.jpg"));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class ItemToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int id)
            {
                if (MediaMetaData.ItemsMetaData.ContainsKey(id))
                {
                    return new BitmapImage(new Uri($"pack://application:,,,/Properties/Icons/{MediaMetaData.ItemsMetaData[id].IconFileName}"));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class SocketToSocketColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Socket socket && socket.SocketedGem != null)
            {
                if (socket.Color == SocketColor.Meta)
                {
                    return new SolidColorBrush(Colors.DarkGray);
                }
                if (socket.Color == SocketColor.Red)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                if (socket.Color == SocketColor.Blue)
                {
                    return new SolidColorBrush(Colors.Blue);
                }
                if (socket.Color == SocketColor.Yellow)
                {
                    return new SolidColorBrush(Color.FromRgb(181, 181, 0));
                }
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
