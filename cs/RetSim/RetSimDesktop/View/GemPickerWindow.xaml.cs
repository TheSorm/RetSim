using RetSim.Items;
using RetSim.Units.UnitStats;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace RetSimDesktop.View
{
    /// <summary>
    /// Interaction logic for GemPickerWindow.xaml
    /// </summary>
    public partial class GemPickerWindow : Window
    {
        public Gem SelectedGem { get; set; }
        public GemPickerWindow(IEnumerable<Gem> gemList)
        {
            InitializeComponent();

            gemGrid.ItemsSource = gemList;

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
            SelectedGem = (Gem)gemGrid.SelectedItem;
            DialogResult = true;
        }
    }
}
