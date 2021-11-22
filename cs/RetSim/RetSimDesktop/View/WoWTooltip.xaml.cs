using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RetSimDesktop
{
    public partial class WoWTooltip : UserControl
    {

        public Window PlacementTarget
        {
            get => (Window)GetValue(PlacementTargetProperty);
            set => SetValue(PlacementTargetProperty, value);
        }

        public static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register(
            "PlacementTarget",
            typeof(Window),
            typeof(WoWTooltip),
            new PropertyMetadata(new PropertyChangedCallback(PlacementTarget_PropertyChanged)));

        public int ItemId
        {
            get => (int)GetValue(ItemIdProperty);
            set => SetValue(ItemIdProperty, value);
        }

        public static readonly DependencyProperty ItemIdProperty = DependencyProperty.Register(
            "ItemId",
            typeof(int),
            typeof(WoWTooltip),
            new PropertyMetadata(new PropertyChangedCallback(TooltipSettings_PropertyChanged)));

        public WoWTooltip()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await Browser.EnsureCoreWebView2Async(null);
            Browser.DefaultBackgroundColor = System.Drawing.Color.Transparent;
            Browser.NavigateToString(@"<head><script>const whTooltips = {colorLinks: false, iconizeLinks: false, renameLinks: false};</script><script src=""https://wow.zamimg.com/widgets/power.js""></script></head><body><script>function test(xPos, yPos) {const event = new MouseEvent('mouseover', {view: window,bubbles: true,cancelable: true, clientX : xPos, clientY : yPos});const cb = document.getElementById('placeholder');const cancelled = !cb.dispatchEvent(event);}</script><a id=""placeholder"" href=""https://tbc.wowhead.com/item=28830""></a></body>");
            await Browser.ExecuteScriptAsync("document.querySelector('body').style.overflow='hidden'");
        }

        private static void PlacementTarget_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WoWTooltip wowTooltip)
            {
                wowTooltip.PlacementTarget.MouseMove += wowTooltip.Parent_MouseMove;
            }
        }

        public static void TooltipSettings_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WoWTooltip wowTooltip)
            {
                wowTooltip.TooltipPopUp.IsOpen = wowTooltip.ItemId != 0;
                wowTooltip.UpdateTooltip(wowTooltip.ItemId);
            }
        }

        private async void UpdateTooltip(int itemId)
        {
            if (Browser != null)
            {
                await Browser.EnsureCoreWebView2Async(null);
                await Browser.ExecuteScriptAsync(@"document.getElementById('placeholder').href = ""https://tbc.wowhead.com/item=" + itemId + @""";");
                //var heightString = await Browser.ExecuteScriptAsync(@"document.getElementsByClassName(""wowhead-tooltip"")[0].clientHeight");
                //var widthString = await Browser.ExecuteScriptAsync(@"document.getElementsByClassName(""wowhead-tooltip"")[0].clientWidth");
                await Browser.ExecuteScriptAsync("test(35, 0);");
                await Browser.ExecuteScriptAsync("document.querySelector('body').style.overflow='hidden'");
            }
        }

        private void Parent_MouseMove(object sender, MouseEventArgs e)
        {
            TooltipPopUp.IsOpen = ItemId != 0;

            var mousePosition = e.GetPosition(PlacementTarget);

            TooltipPopUp.HorizontalOffset = mousePosition.X + 1;
            TooltipPopUp.VerticalOffset = mousePosition.Y + 1;
        }
    }
}
