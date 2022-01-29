using RetSim.Data;
using RetSim.Items;
using RetSim.Units.Enemy;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            Initialized += Setup;

            InitializeComponent();

            string line1 = "The maximum amount of time the sim will be allowed to delay Crusader Strike by, expressed in milliseconds.";
            string line2 = "This setting should only be changed by advanced users. Use the CS Delay sim to find your optimal CS delay.";
            string line3 = "Leave at 0 if you don't understand what you're doing.";

            MaxCSDelay.ToolTip = new ToolTip { Content = $"{line1}\n\n{line2}\n\n{line3}" };
        }

        private void Setup(object? sender, EventArgs e)
        {
            foreach (var boss in Collections.Bosses)
            {
                BossesComboBox.Items.Add(boss);
            }
        }

        private void RedSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Red, Items.GemsSorted);
        }
        private void YellowSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Yellow, Items.GemsSorted);
        }
        private void BlueSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Blue, Items.GemsSorted);
        }
        private void MetaSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Meta, Items.MetaGems.Values, true);
        }

        private void SelectGemForSocketColor(SocketColor color, IEnumerable<Gem> gemList, bool meta = false)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                GemPickerWindow gemPicker = new(gemList, null, meta);
                if (gemPicker.ShowDialog() == true)
                {
                    foreach (var displayItem in retSimUIModel.AllGear.Values)
                    {
                        for (int i = 0; i < displayItem.Item.Sockets.Length; i++)
                        {
                            if (displayItem.Item.Sockets[i] != null && displayItem.Item.Sockets[i].Color == color)
                            {
                                displayItem.Item.Sockets[i].SocketedGem = gemPicker.SelectedGem;
                                displayItem.OnPropertyChanged("");
                            }
                        }
                    }
                    foreach (var displayWeapon in retSimUIModel.AllWeapons.Values)
                    {
                        for (int i = 0; i < displayWeapon.Item.Sockets.Length; i++)
                        {
                            if (displayWeapon.Item.Sockets[i] != null && displayWeapon.Item.Sockets[i].Color == color)
                            {
                                displayWeapon.Item.Sockets[i].SocketedGem = gemPicker.SelectedGem;
                                displayWeapon.OnPropertyChanged("");
                            }
                        }
                    }

                    retSimUIModel.SelectedGear.OnPropertyChanged("");
                }
            }
        }

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AirTotem1_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.SelectedConsumables.SelectedTemporaryWeaponEnchantment = Model.TemporaryWeaponEnchantment.None;
            }
        }
        private void TemporaryWeaponEnchantment_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                if (retSimUIModel.SelectedConsumables.SelectedTemporaryWeaponEnchantment != Model.TemporaryWeaponEnchantment.None)
                {
                    retSimUIModel.SelectedBuffs.AirTotem1Enabled = false;
                }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.Reset();
            }
        }
    }

    public class BossToIDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int bossID)
            {
                return Collections.Bosses[bossID];
            }
            return Collections.Bosses[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Boss boss)
            {
                return boss.ID;
            }
            return 0;
        }
    }

    public class ComparisonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(true) == true ? parameter : Binding.DoNothing;
        }
    }

    public class StringEmptyOrZeroConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(
              object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty((string)value) ? parameter : (((string)value).TrimStart('0').Length == 0 ? parameter : ((string)value).TrimStart('0'));
        }

    }
}
