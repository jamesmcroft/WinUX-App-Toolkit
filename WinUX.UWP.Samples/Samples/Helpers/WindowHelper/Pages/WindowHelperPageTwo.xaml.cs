namespace WinUX.UWP.Samples.Samples.Helpers.WindowHelper.Pages
{
    using Windows.UI.Xaml.Navigation;

    public sealed partial class WindowHelperPageTwo
    {
        public WindowHelperPageTwo()
        {
            this.InitializeComponent();
        }

        public WindowHelperPageViewModel ViewModel => this.DataContext as WindowHelperPageViewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }

            if (this.DataContext == null)
            {
                this.DataContext = new WindowHelperPageViewModel { IsSecondaryView = true };
            }

            base.OnNavigatedTo(e);
        }
    }
}