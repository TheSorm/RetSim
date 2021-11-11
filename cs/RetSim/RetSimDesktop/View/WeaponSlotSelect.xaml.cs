using RetSim.Items;
using RetSim.Units.UnitStats;
using RetSimDesktop.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearSlotSelect.xaml
    /// </summary>
    public partial class WeaponSlotSelect : UserControl
    {
        public List<EquippableWeapon> WeaponList
        {
            get => (List<EquippableWeapon>)GetValue(WeaponListProperty);
            set => SetValue(WeaponListProperty, value);
        }

        public static readonly DependencyProperty WeaponListProperty = DependencyProperty.Register(
            "WeaponList",
            typeof(List<EquippableWeapon>),
            typeof(WeaponSlotSelect));

        public EquippableWeapon SelectedItem
        {
            get => (EquippableWeapon)GetValue(SelectedItemProperty);
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(EquippableWeapon),
            typeof(WeaponSlotSelect),
            new PropertyMetadata(null, CheckIfSelectionIsPresent));

        public WeaponSlotSelect()
        {
            InitializeComponent();

            gearSlot.SetBinding(DataGrid.ItemsSourceProperty, new Binding("WeaponList")
            {
                Source = this,
                Mode = BindingMode.OneWay
            });

            gearSlot.SetBinding(DataGrid.SelectedItemProperty, new Binding("SelectedItem")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            });

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
            Binding hasteBinding = new("Stats[" + StatName.HasteRating + "]");
            hasteBinding.Converter = statConverter;
            HasteColumn.Binding = hasteBinding;
            Binding expBinding = new("Stats[" + StatName.ExpertiseRating + "]");
            expBinding.Converter = statConverter;
            ExpColumn.Binding = expBinding;
            Binding apenBinding = new("Stats[" + StatName.ArmorPenetration + "]");
            apenBinding.Converter = statConverter;
            APenColumn.Binding = apenBinding;
            Binding staBinding = new("Stats[" + StatName.Stamina + "]");
            staBinding.Converter = statConverter;
            StaColumn.Binding = staBinding;
            Binding intBinding = new("Stats[" + StatName.Intellect + "]");
            intBinding.Converter = statConverter;
            IntColumn.Binding = intBinding;
            Binding mp5Binding = new("Stats[" + StatName.ManaPer5 + "]");
            mp5Binding.Converter = statConverter;
            MP5Column.Binding = mp5Binding;
            Binding spBinding = new("Stats[" + StatName.SpellPower + "]");
            spBinding.Converter = statConverter;
            SPColumn.Binding = spBinding;
            Binding sCritBinding = new("Stats[" + StatName.SpellCritRating + "]");
            sCritBinding.Converter = statConverter;
            SCritColumn.Binding = sCritBinding;
            Binding sHitBinding = new("Stats[" + StatName.SpellHitRating + "]");
            sHitBinding.Converter = statConverter;
            SHitColumn.Binding = sHitBinding;
            Binding sHasteBinding = new("Stats[" + StatName.SpellHasteRating + "]");
            sHasteBinding.Converter = statConverter;
            SHasteColumn.Binding = sHasteBinding;

        }

        private void CheckIfSelectionIsPresent()
        {
            if (!WeaponList.Contains(SelectedItem))
            {
                BindingOperations.ClearBinding(gearSlot, DataGrid.SelectedItemProperty);
                gearSlot.SelectedCells.Clear();
            }
            else
            {
                gearSlot.SelectedItem = SelectedItem;
                gearSlot.SetBinding(DataGrid.SelectedItemProperty, new Binding("SelectedItem")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                });
            }
        }

        private void GearSlotSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = (EquippableWeapon)gearSlot.SelectedItem;
            gearSlot.SetBinding(DataGrid.SelectedItemProperty, new Binding("SelectedItem")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            });
        }

        private static void CheckIfSelectionIsPresent(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WeaponSlotSelect select)
            {
                select.CheckIfSelectionIsPresent();
            }
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGridCellTarget = (DataGridCell)sender;
            var header = dataGridCellTarget.Column.Header.ToString();

            Socket? selectedSocket = null;
            if (header == "Socket 1")
            {
                selectedSocket = SelectedItem.Socket1;
            }
            else if (header == "Socket 2")
            {
                selectedSocket = SelectedItem.Socket2;
            }
            else if (header == "Socket 3")
            {
                selectedSocket = SelectedItem.Socket3;
            }

            if (selectedSocket != null)
            {
                GemPickerWindow gemPicker;
                if (selectedSocket.Color == SocketColor.Meta)
                {
                    gemPicker = new(RetSim.Data.Items.MetaGems.Values);
                }
                else
                {
                    gemPicker = new(RetSim.Data.Items.Gems.Values);
                }

                if (gemPicker.ShowDialog() == true)
                {
                    selectedSocket.SocketedGem = gemPicker.SelectedGem;
                    gearSlot.Items.Refresh();
                }
            }
        }
    }

    public class WeaponSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (((int)value) / 1000f).ToString("0.#");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0f;
        }
    }

    public class WeaponDPSConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((float)value).ToString("0.##");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0f;
        }
    }
}

