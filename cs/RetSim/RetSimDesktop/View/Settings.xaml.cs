using RetSim.Items;
using RetSimDesktop.View;
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
            InitializeComponent();
        }

        private void RedSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Red, RetSim.Data.Items.Gems.Values);
        }
        private void YellowSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Yellow, RetSim.Data.Items.Gems.Values);
        }
        private void BlueSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Blue, RetSim.Data.Items.Gems.Values);
        }
        private void MetaSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Meta, RetSim.Data.Items.MetaGems.Values);
        }

        private void SelectGemForSocketColor(SocketColor color, IEnumerable<Gem> gemList)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                GemPickerWindow gemPicker = new(gemList, null);
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
                        for (int i = 0; i < displayWeapon.Weapon.Sockets.Length; i++)
                        {
                            if (displayWeapon.Weapon.Sockets[i] != null && displayWeapon.Weapon.Sockets[i].Color == color)
                            {
                                displayWeapon.Weapon.Sockets[i].SocketedGem = gemPicker.SelectedGem;
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
            return string.IsNullOrEmpty((string)value) ? parameter : (((string)value).TrimStart('0').Length == 0 ? parameter : ((string)value).TrimStart('0'));
        }

        public object ConvertBack(
              object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

    }
}
