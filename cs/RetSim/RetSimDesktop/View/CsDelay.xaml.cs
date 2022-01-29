using RetSim.Items;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for StatWeights.xaml
    /// </summary>
    public partial class CsDelay : UserControl
    {
        private static CsDelayWorker csDelayWorker = new();
        private DisplayGear? currentWeapon;
        public CsDelay()
        {
            InitializeComponent();

            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SelectedGear.PropertyChanged += SelectedGearChanged;
                    retSimUIModel.SimButtonStatus.PropertyChanged += SelectedGearChanged;
                    SelectedGearChanged(this, new("SelectedWeapon"));
                }
            };
        }

        private void SelectedGearChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedWeapon" || e.PropertyName == "IsSimButtonEnabled")
            {
                csDelayGrid.Dispatcher.Invoke(() =>
                {
                    if (DataContext is RetSimUIModel retSimUIModel && retSimUIModel.SelectedGear.SelectedWeapon != null
                    && retSimUIModel.SimButtonStatus.IsSimButtonEnabled && currentWeapon != retSimUIModel.SelectedGear.SelectedWeapon
                    && retSimUIModel.SelectedGear.SelectedWeapon.Item is EquippableWeapon equippedWeapon)
                    {
                        retSimUIModel.DisplayCsDelay.Clear();

                        for (int i = 0; i < equippedWeapon.AttackSpeed / 100; i++)
                        {
                            retSimUIModel.DisplayCsDelay.Add(new() { Delay = i / 10f, DpsDelta = 0, EnabledForCsDelay = true });
                        }

                        csDelayGrid.Items.Refresh();
                        currentWeapon = retSimUIModel.SelectedGear.SelectedWeapon;
                    }

                });
            }
        }

        private void ChkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                foreach (var displayStatWeight in retSimUIModel.DisplayCsDelay)
                {
                    displayStatWeight.EnabledForCsDelay = true;
                }
            }
        }

        private void ChkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                foreach (var displayStatWeight in retSimUIModel.DisplayCsDelay)
                {
                    displayStatWeight.EnabledForCsDelay = false;
                }
            }
        }

        private void StatWeightSimButton_Click(object sender, RoutedEventArgs e)
        {
            if (!csDelayWorker.IsBusy && DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.SimButtonStatus.IsSimButtonEnabled = false;
                csDelayWorker.RunWorkerAsync(retSimUIModel);
            }
        }
    }
}
