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
            var (Weapons, Armor, Sets, Gems, MetaGems, Enchants) = RetSim.Data.Manager.LoadData();
            RetSim.Data.Items.Initialize(Weapons, Armor, Sets, Gems, MetaGems, Enchants);
            RetSim.Data.Manager.InstantiateData();
            InitializeComponent();
            RetSimUIModel GM = RetSimUIModel.Load();
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
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SimButtonStatus.IsGearSimButtonEnabled = false;
                    retSimUIModel.SimButtonStatus.IsSimButtonEnabled = false;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.Save();
            }
        }
    }
}
