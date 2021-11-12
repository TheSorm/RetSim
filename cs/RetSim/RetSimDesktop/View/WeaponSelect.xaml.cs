using RetSim.Items;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearSelect.xaml
    /// </summary>
    /// 
    public partial class WeaponSelect : UserControl
    {
        private Dictionary<WeaponType, WeaponSlotSelect> SelectorByType = new();
        public Dictionary<WeaponType, List<WeaponDPS>> ShownWeapons { get; set; }
        public List<WeaponDPS> AllShownWeapons { get; set; }

        public WeaponSelect()
        {
            InitializeComponent();
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SelectedPhases.PropertyChanged += Model_PropertyChanged;
                    Model_PropertyChanged(this, new PropertyChangedEventArgs(""));
                }
            };
            ShownWeapons = new();
            AllShownWeapons = new();
            SelectorByType.Add(WeaponType.Sword, SwordSelect);
            SelectorByType.Add(WeaponType.Mace, MaceSelect);
            SelectorByType.Add(WeaponType.Axe, AxeSelect);
            SelectorByType.Add(WeaponType.Polearm, PolearmSelect);
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                foreach (var type in SelectorByType.Keys)
                {
                    ShownWeapons[type] = new();
                    if (retSimUIModel.SelectedPhases.Phase1Selected && retSimUIModel.WeaponsByPhases[type].ContainsKey(1))
                    {
                        ShownWeapons[type].AddRange(retSimUIModel.WeaponsByPhases[type][1]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase2Selected && retSimUIModel.WeaponsByPhases[type].ContainsKey(2))
                    {
                        ShownWeapons[type].AddRange(retSimUIModel.WeaponsByPhases[type][2]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase3Selected && retSimUIModel.WeaponsByPhases[type].ContainsKey(3))
                    {
                        ShownWeapons[type].AddRange(retSimUIModel.WeaponsByPhases[type][3]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase4Selected && retSimUIModel.WeaponsByPhases[type].ContainsKey(4))
                    {
                        ShownWeapons[type].AddRange(retSimUIModel.WeaponsByPhases[type][4]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase5Selected && retSimUIModel.WeaponsByPhases[type].ContainsKey(5))
                    {
                        ShownWeapons[type].AddRange(retSimUIModel.WeaponsByPhases[type][5]);
                    }

                    SelectorByType[type].SetBinding(WeaponSlotSelect.WeaponListProperty, new Binding("ShownWeapons[" + type + "]")
                    {
                        Source = this,
                        Mode = BindingMode.OneWay
                    });

                    SelectorByType[type].LevelColumn.SortDirection = ListSortDirection.Descending;
                    SelectorByType[type].gearSlot.Items.SortDescriptions.Add(new SortDescription(SelectorByType[type].LevelColumn.SortMemberPath, ListSortDirection.Descending));

                    SelectorByType[type].SetBinding(WeaponSlotSelect.WeaponEnchantListProperty, new Binding("EnchantsBySlot[" + Slot.Weapon + "]")
                    {
                        Source = DataContext,
                        Mode = BindingMode.OneWay
                    });
                }

                AllShownWeapons = new();
                foreach (var weapons in ShownWeapons.Values)
                {
                    AllShownWeapons.AddRange(weapons);
                }
                AllWeaponSelect.SetBinding(WeaponSlotSelect.WeaponListProperty, new Binding("AllShownWeapons")
                {
                    Source = this,
                    Mode = BindingMode.OneWay
                });
                AllWeaponSelect.LevelColumn.SortDirection = ListSortDirection.Descending;
                AllWeaponSelect.gearSlot.Items.SortDescriptions.Add(new SortDescription(AllWeaponSelect.LevelColumn.SortMemberPath, ListSortDirection.Descending));

                AllWeaponSelect.SetBinding(WeaponSlotSelect.WeaponEnchantListProperty, new Binding("EnchantsBySlot[" + Slot.Weapon + "]")
                {
                    Source = DataContext,
                    Mode = BindingMode.OneWay
                });
            }
        }
    }
}
