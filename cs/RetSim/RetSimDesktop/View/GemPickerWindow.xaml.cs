using RetSim.Items;
using RetSim.Units.UnitStats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GemPickerWindow.xaml
    /// </summary>
    public partial class GemPickerWindow : Window
    {
        public Gem? SelectedGem { get; set; }
        public GemPickerWindow(IEnumerable<Gem> gemList, Gem selectedGem, bool meta = false)
        {
            InitializeComponent();

            gemGrid.ItemsSource = gemList;
            gemGrid.SelectedItem = selectedGem;

            StatConverter statConverter = new();

            Binding strBinding = new("Stats[" + StatName.Strength + "]");
            strBinding.Converter = statConverter;
            StrColumn.Binding = strBinding;
            Binding apBinding = new("Stats[" + StatName.AttackPower + "]");
            apBinding.Converter = statConverter;
            APColumn.Binding = apBinding;
            Binding agiBinding = new("Stats[" + StatName.Agility + "]");
            agiBinding.Converter = statConverter;
            AgiColumn.Binding = agiBinding;
            Binding critBinding = new("Stats[" + StatName.CritRating + "]");
            critBinding.Converter = statConverter;
            CritColumn.Binding = critBinding;
            Binding hitBinding = new("Stats[" + StatName.HitRating + "]");
            hitBinding.Converter = statConverter;
            HitColumn.Binding = hitBinding;
            Binding staBinding = new("Stats[" + StatName.Stamina + "]");
            staBinding.Converter = statConverter;
            StaColumn.Binding = staBinding;
            Binding mp5Binding = new("Stats[" + StatName.ManaPer5 + "]");
            mp5Binding.Converter = statConverter;
            MP5Column.Binding = mp5Binding;
            Binding spBinding = new("Stats[" + StatName.SpellPower + "]");
            spBinding.Converter = statConverter;

            if (meta)
            {
                NameColumn.SortDirection = ListSortDirection.Ascending;
                gemGrid.Items.SortDescriptions.Add(new SortDescription(NameColumn.SortMemberPath, ListSortDirection.Ascending));
            }
        }

        private void GemGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) is not DataGridRow row) return;

            if (row.Item is Gem gem)
            {
                if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://tbc.wowhead.com/item=" + gem.ID,
                        UseShellExecute = true
                    });
                }
                else if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
                {
                    Tooltip.Browser.Dispose();
                    Tooltip.Browser.Visibility = Visibility.Collapsed;
                    Tooltip.Browser = null;
                    SelectedGem = gem;
                    DialogResult = true;
                }
            }
        }

        private void DataGridCell_MouseEnter(object sender, MouseEventArgs e)
        {
            if (SelectedGem == null)
            {
                if (sender is DataGridCell cell)
                {
                    if (DataGridRow.GetRowContainingElement(cell).Item is Gem gem)
                    {
                        Tooltip.ItemId = gem.ID;
                        WoWTooltip.TooltipSettings_PropertyChanged(Tooltip, new DependencyPropertyChangedEventArgs());
                    }
                }
            }
        }

        private void DataGridCell_MouseLeave(object sender, MouseEventArgs e)
        {

            Tooltip.ItemId = 0;
            WoWTooltip.TooltipSettings_PropertyChanged(Tooltip, new DependencyPropertyChangedEventArgs());

        }

        private void GemPicker_Closing(object sender, CancelEventArgs e)
        {
            if (Tooltip.Browser != null)
            {
                Tooltip.Browser.Dispose();
                Tooltip.Browser.Visibility = Visibility.Collapsed;
                Tooltip.Browser = null;
            }
        }
    }
    public class GemToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int id)
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Properties/Icons/{MediaMetaData.GemsToIconName[id]}"));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
