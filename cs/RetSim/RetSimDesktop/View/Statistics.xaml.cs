using RetSimDesktop.ViewModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        public Statistics()
        {
            InitializeComponent();
        }

        private void CombatLogSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CombatLogSelection.SelectedValue != null)
            {
                var value = CombatLogSelection.SelectedValue.ToString();

                if (value == "Min")
                {
                    CombatLogTable.ItemsSource = (DataContext as RetSimUIModel).CurrentSimOutput.MinCombatLog;
                }
                else if (value == "Median")
                {
                    CombatLogTable.ItemsSource = (DataContext as RetSimUIModel).CurrentSimOutput.MedianCombatLog;
                }
                else if (value == "Max")
                {
                    CombatLogTable.ItemsSource = (DataContext as RetSimUIModel).CurrentSimOutput.MaxCombatLog;
                }
            }
        }
    }
}
