using RetSimDesktop.View;
using RetSimDesktop.ViewModel;
using System.Windows;
using System.Windows.Input;

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
            InitializeAsync();
            GM.TooltipSettings.PropertyChanged += TooltipSettings_PropertyChanged;
        }

        async void InitializeAsync()
        {
            await browser.EnsureCoreWebView2Async(null);
            browser.DefaultBackgroundColor = System.Drawing.Color.Transparent;
            browser.NavigateToString(@"<head><script>const whTooltips = {colorLinks: true, iconizeLinks: false, renameLinks: false};</script><script src=""https://wow.zamimg.com/widgets/power.js""></script></head><body><script>function test(xPos, yPos) {const event = new MouseEvent('mouseover', {view: window,bubbles: true,cancelable: true, clientX : xPos, clientY : yPos});const cb = document.getElementById('placeholder');const cancelled = !cb.dispatchEvent(event);}</script><a id=""placeholder"" href=""https://tbc.wowhead.com/item=28830""></a></body>");
            await browser.ExecuteScriptAsync("document.querySelector('body').style.overflow='hidden'");
        }

        private void TooltipSettings_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                TooltipPopUp.IsOpen = retSimUIModel.TooltipSettings.HoverItemID != 0;
                UpdateTooltip(retSimUIModel, true);
            }
        }

        private void popup_MouseMove(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                TooltipPopUp.IsOpen = retSimUIModel.TooltipSettings.HoverItemID != 0;

                var mousePosition = e.GetPosition(Window);
                if (mousePosition.Y > Window.Height / 2 + TooltipPopUp.Height / 2)
                {
                    this.TooltipPopUp.HorizontalOffset = mousePosition.X - 30;
                    this.TooltipPopUp.VerticalOffset = mousePosition.Y - TooltipPopUp.Height - 1;
                    UpdateTooltip(retSimUIModel, true);
                }
                else
                {
                    this.TooltipPopUp.HorizontalOffset = mousePosition.X + 1;
                    this.TooltipPopUp.VerticalOffset = mousePosition.Y + 1;
                    UpdateTooltip(retSimUIModel, false);
                }
            }
        }

        private async void UpdateTooltip(RetSimUIModel retSimUIModel, bool bottom)
        {
            await browser.EnsureCoreWebView2Async(null);
            await browser.ExecuteScriptAsync(@"document.getElementById('placeholder').href = ""https://tbc.wowhead.com/item=" + retSimUIModel.TooltipSettings.HoverItemID + @""";");
            if (!bottom)
            {
                await browser.ExecuteScriptAsync("test(30, 0);");
            }
            else
            {
                await browser.ExecuteScriptAsync("test(30, " + TooltipPopUp.Height + ");");
            }
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
