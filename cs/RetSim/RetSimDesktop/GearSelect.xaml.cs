using RetSim.Data;
using RetSim.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearSelect.xaml
    /// </summary>
    public partial class GearSelect : UserControl
    {
        public GearSelect()
        {
            InitializeComponent();

            HeadSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Heads,
                Mode = BindingMode.OneWay
            });

            NeckSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Necks,
                Mode = BindingMode.OneWay
            });

            ShouldersSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Shoulders,
                Mode = BindingMode.OneWay
            });
            BackSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Cloaks,
                Mode = BindingMode.OneWay
            });
            ChestSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Chests,
                Mode = BindingMode.OneWay
            });
            WristSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Wrists,
                Mode = BindingMode.OneWay
            });
            HandsSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Hands,
                Mode = BindingMode.OneWay
            });
            WaistSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Waists,
                Mode = BindingMode.OneWay
            });
            LegsSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Legs,
                Mode = BindingMode.OneWay
            });
            Finger1Select.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Fingers,
                Mode = BindingMode.OneWay
            });
            Finger2Select.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Fingers,
                Mode = BindingMode.OneWay
            });
            Trinket1Select.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Trinkets,
                Mode = BindingMode.OneWay
            });
            Trinket2Select.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Trinkets,
                Mode = BindingMode.OneWay
            });
            RelicSelect.SetBinding(GearSlotSelect.SlotListProperty, new Binding("Values")
            {
                Source = Items.Relics,
                Mode = BindingMode.OneWay
            });
        }
    }
}
