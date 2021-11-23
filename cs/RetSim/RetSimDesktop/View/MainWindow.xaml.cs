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
            InitializeComponent();
            RetSimUIModel GM = RetSimUIModel.Load();
            DataContext = GM;

            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += OnTick;
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
