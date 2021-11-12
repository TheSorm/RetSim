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
    public partial class GearSelect : UserControl
    {
        private Dictionary<Slot, List<GearSlotSelect>> SelectorBySlot = new();
        public Dictionary<Slot, List<ItemDPS>> ShownGear { get; set; }

        public GearSelect()
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
            ShownGear = new();
            SelectorBySlot.Add(Slot.Head, new() { HeadSelect });
            SelectorBySlot.Add(Slot.Neck, new() { NeckSelect });
            SelectorBySlot.Add(Slot.Shoulders, new() { ShouldersSelect });
            SelectorBySlot.Add(Slot.Back, new() { BackSelect });
            SelectorBySlot.Add(Slot.Chest, new() { ChestSelect });
            SelectorBySlot.Add(Slot.Wrists, new() { WristSelect });
            SelectorBySlot.Add(Slot.Hands, new() { HandsSelect });
            SelectorBySlot.Add(Slot.Waist, new() { WaistSelect });
            SelectorBySlot.Add(Slot.Legs, new() { LegsSelect });
            SelectorBySlot.Add(Slot.Feet, new() { FeetSelect });
            SelectorBySlot.Add(Slot.Finger, new() { Finger1Select, Finger2Select });
            SelectorBySlot.Add(Slot.Trinket, new() { Trinket1Select, Trinket2Select });
            SelectorBySlot.Add(Slot.Relic, new() { RelicSelect });
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                foreach (var slot in SelectorBySlot.Keys)
                {
                    ShownGear[slot] = new();
                    if (retSimUIModel.SelectedPhases.Phase1Selected && retSimUIModel.GearByPhases[slot].ContainsKey(1))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][1]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase2Selected && retSimUIModel.GearByPhases[slot].ContainsKey(2))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][2]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase3Selected && retSimUIModel.GearByPhases[slot].ContainsKey(3))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][3]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase4Selected && retSimUIModel.GearByPhases[slot].ContainsKey(4))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][4]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase5Selected && retSimUIModel.GearByPhases[slot].ContainsKey(5))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][5]);
                    }
                    foreach (var itemSelector in SelectorBySlot[slot])
                    {
                        itemSelector.SetBinding(GearSlotSelect.SlotListProperty, new Binding("ShownGear[" + slot + "]")
                        {
                            Source = this,
                            Mode = BindingMode.OneWay
                        });

                        itemSelector.LevelColumn.SortDirection = ListSortDirection.Descending;
                        itemSelector.gearSlot.Items.SortDescriptions.Add(new SortDescription(itemSelector.LevelColumn.SortMemberPath, ListSortDirection.Descending));
                    }
                }
            }
        }
    }
}
