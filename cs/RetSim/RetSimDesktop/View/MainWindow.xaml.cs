using RetSimDesktop.View;
using RetSimDesktop.ViewModel;
using System.Windows;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static SimWorker simWorker = new();
        public MainWindow()
        {
            var (Weapons, Armor, Sets, Gems, MetaGems, Enchants) = RetSim.Data.Importer.LoadData();
            RetSim.Data.Items.Initialize(Weapons, Armor, Sets, Gems, MetaGems, Enchants);
            InitializeComponent();
            RetSimUIModel GM = new();
            DataContext = GM;
        }


        private void Gear_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Hidden;
            GearSelect.Visibility = Visibility.Visible;
            GearButton.IsEnabled = false;
            SettingsButton.IsEnabled = true;
            StatisticsButton.IsEnabled = true;

            //GearSelect.WeaponSelect.AllWeaponSelect.Items.Refresh();
            GearSelect.HeadSelect.gearSlot.Items.Refresh();
            GearSelect.NeckSelect.gearSlot.Items.Refresh();
            GearSelect.ShouldersSelect.gearSlot.Items.Refresh();
            GearSelect.BackSelect.gearSlot.Items.Refresh();
            GearSelect.ChestSelect.gearSlot.Items.Refresh();
            GearSelect.WristSelect.gearSlot.Items.Refresh();
            GearSelect.HandsSelect.gearSlot.Items.Refresh();
            GearSelect.WaistSelect.gearSlot.Items.Refresh();
            GearSelect.LegsSelect.gearSlot.Items.Refresh();
            GearSelect.FeetSelect.gearSlot.Items.Refresh();
            GearSelect.Finger1Select.gearSlot.Items.Refresh();
            GearSelect.Finger2Select.gearSlot.Items.Refresh();
            GearSelect.Trinket1Select.gearSlot.Items.Refresh();
            GearSelect.Trinket2Select.gearSlot.Items.Refresh();
            GearSelect.RelicSelect.gearSlot.Items.Refresh();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Visible;
            Statistics.Visibility = Visibility.Hidden;
            GearSelect.Visibility = Visibility.Hidden;
            GearButton.IsEnabled = true;
            SettingsButton.IsEnabled = false;
            StatisticsButton.IsEnabled = true;
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Visible;
            GearSelect.Visibility = Visibility.Hidden;
            GearButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;
            StatisticsButton.IsEnabled = false;
        }

        private void DpsSimClick(object sender, RoutedEventArgs e)
        {
            if (!simWorker.IsBusy)
            {
                simWorker.RunWorkerAsync(DataContext);
            }
        }
    }
}
