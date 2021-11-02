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
    /// Interaction logic for GearSlotSelect.xaml
    /// </summary>
    public partial class GearSlotSelect : UserControl
    {
        public string SlotName { get; set; }
        public GearSlotSelect()
        {
            InitializeComponent();

            gearSlot.ItemsSource = SlotName switch
            {
                "Head" => RetSim.Data.Items.Heads.Values,
                _ => RetSim.Data.Items.Heads.Values,
            };
        }
    }
}
