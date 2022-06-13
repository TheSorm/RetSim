using RetSimDesktop.View;
using RetSimDesktop.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static SimWorker simWorker = new();

        private DispatcherTimer timer = new DispatcherTimer();
        private Stopwatch timeTaken = new();
        public MainWindow()
        {
            var (Weapons, Armor, Sets, Gems, MetaGems, Enchants) = RetSim.Data.Manager.LoadData();
            RetSim.Data.Items.Initialize(Weapons, Armor, Sets, Gems, MetaGems, Enchants);
            RetSim.Data.Manager.InstantiateData();
            MediaMetaData.Initialize();

            InitializeComponent();
            RetSimUIModel GM = RetSimUIModel.Load();
            DataContext = GM;

            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += OnTick;
        }

        private void Gear_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
            StatWeights.Visibility = Visibility.Hidden;
            CsDelay.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Hidden;
            GearSelect.Visibility = Visibility.Visible; 
            PlayerImport.Visibility = Visibility.Hidden;
            PlayerImportButton.IsEnabled = true;
            GearButton.IsEnabled = false;
            SettingsButton.IsEnabled = true;
            CsDelayButton.IsEnabled = true;
            StatWeightsButton.IsEnabled = true;
            StatisticsButton.IsEnabled = true;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Visible;
            StatWeights.Visibility = Visibility.Hidden;
            CsDelay.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Hidden;
            GearSelect.Visibility = Visibility.Hidden;
            PlayerImport.Visibility = Visibility.Hidden;
            PlayerImportButton.IsEnabled = true;
            GearButton.IsEnabled = true;
            SettingsButton.IsEnabled = false;
            CsDelayButton.IsEnabled = true;
            StatWeightsButton.IsEnabled = true;
            StatisticsButton.IsEnabled = true;
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
            StatWeights.Visibility = Visibility.Hidden;
            CsDelay.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Visible;
            GearSelect.Visibility = Visibility.Hidden;
            PlayerImport.Visibility = Visibility.Hidden;
            PlayerImportButton.IsEnabled = true;
            GearButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;
            CsDelayButton.IsEnabled = true;
            StatWeightsButton.IsEnabled = true;
            StatisticsButton.IsEnabled = false;
        }

        private void StatWeights_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
            StatWeights.Visibility = Visibility.Visible;
            CsDelay.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Hidden;
            GearSelect.Visibility = Visibility.Hidden;
            PlayerImport.Visibility = Visibility.Hidden;
            PlayerImportButton.IsEnabled = true;
            GearButton.IsEnabled = true;
            CsDelayButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;
            StatWeightsButton.IsEnabled = false;
            StatisticsButton.IsEnabled = true;
        }

        private void CsDelay_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
            CsDelay.Visibility = Visibility.Visible;
            StatWeights.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Hidden;
            GearSelect.Visibility = Visibility.Hidden;
            PlayerImport.Visibility = Visibility.Hidden;
            PlayerImportButton.IsEnabled = true;
            GearButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;
            StatWeightsButton.IsEnabled = true;
            CsDelayButton.IsEnabled = false;
            StatisticsButton.IsEnabled = true;
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
            StatWeights.Visibility = Visibility.Hidden;
            CsDelay.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Hidden;
            GearSelect.Visibility = Visibility.Hidden;
            PlayerImport.Visibility = Visibility.Visible;
            PlayerImportButton.IsEnabled = false;
            GearButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;
            CsDelayButton.IsEnabled = true;
            StatWeightsButton.IsEnabled = true;
            StatisticsButton.IsEnabled = true;         
        }

        public void SwitchToGearSelection(int slot)
        {
            Gear_Click(null, null);
            GearSelect.SwitchToSlotSelection(slot);
        }

        private void DpsSimClick(object sender, RoutedEventArgs e)
        {
            if (!simWorker.IsBusy)
            {
                simWorker.RunWorkerAsync(DataContext);
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SimButtonStatus.IsSimButtonEnabled = false;
                }
                SimButton.SetBinding(Button.ContentProperty, "CurrentSimOutput.Progress");
                SimButton.ContentStringFormat = "{0}%";
                timer.Start();
                timeTaken.Restart();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.Save();
            }

            WoWTooltip.Browser.Dispose();
            WoWTooltip.Browser.Visibility = Visibility.Collapsed;
            WoWTooltip.Browser = null;
        }

        private void OnTick(object? sender, EventArgs e)
        {
            SimTimeTaken.Content = timeTaken.ElapsedMilliseconds / 1000f;
        }

        private void SimButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (SimButton.IsEnabled)
            {
                SimButton.Content = "Sim";
                SimButton.ContentStringFormat = "";
                timer.Stop();
                timeTaken.Stop();
            }
        }


    }
}
