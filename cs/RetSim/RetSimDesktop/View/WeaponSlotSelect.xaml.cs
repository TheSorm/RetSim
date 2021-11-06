using RetSim.Items;
using RetSim.Units.UnitStats;
using RetSimDesktop.View;
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
        public IEnumerable<EquippableWeapon> WeaponList
        {
            get => (IEnumerable<EquippableWeapon>)GetValue(WeaponListProperty);
            set => SetValue(WeaponListProperty, value);
        }

        public static readonly DependencyProperty WeaponListProperty = DependencyProperty.Register(
            "WeaponList",
            typeof(IEnumerable<EquippableWeapon>),
            typeof(WeaponSlotSelect));

        public EquippableWeapon SelectedItem
        {
            get => (EquippableWeapon)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(EquippableWeapon),
            typeof(WeaponSlotSelect));

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

            StrColumn.Binding = new Binding("Stats[" + StatName.Strength + "]");
            APColumn.Binding = new Binding("Stats[" + StatName.AttackPower + "]");
            AgiColumn.Binding = new Binding("Stats[" + StatName.Agility + "]");
            CritColumn.Binding = new Binding("Stats[" + StatName.CritRating + "]");
            HitColumn.Binding = new Binding("Stats[" + StatName.HitRating + "]");
            HasteColumn.Binding = new Binding("Stats[" + StatName.HasteRating + "]");
            ExpColumn.Binding = new Binding("Stats[" + StatName.ExpertiseRating + "]");
            APenColumn.Binding = new Binding("Stats[" + StatName.ArmorPenetration + "]");
            StaColumn.Binding = new Binding("Stats[" + StatName.Stamina + "]");
            IntColumn.Binding = new Binding("Stats[" + StatName.Intellect + "]");
            MP5Column.Binding = new Binding("Stats[" + StatName.ManaPer5 + "]");
            SPColumn.Binding = new Binding("Stats[" + StatName.SpellPower + "]");
            SCritColumn.Binding = new Binding("Stats[" + StatName.SpellCritRating + "]");
            SHitColumn.Binding = new Binding("Stats[" + StatName.SpellHitRating + "]");
            SHasteColumn.Binding = new Binding("Stats[" + StatName.SpellHasteRating + "]");
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
}

