using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
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
    /// Interaction logic for StatWeights.xaml
    /// </summary>
    public partial class StatWeights : UserControl
    {
        private static StatWeightWorker statWeightWorker = new();
        public StatWeights()
        {
            InitializeComponent();
        }

        private void StatWeightSimButton_Click(object sender, RoutedEventArgs e)
        {
            if (!statWeightWorker.IsBusy && DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.SimButtonStatus.IsSimButtonEnabled = false;
                statWeightWorker.RunWorkerAsync(retSimUIModel);
            }
        }
    }
}
