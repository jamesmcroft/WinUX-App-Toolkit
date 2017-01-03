namespace WinUX.UWP.Samples.Samples.Helpers.WindowHelper.Pages
{
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WindowHelperPageOne
    {
        public WindowHelperPageOne()
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