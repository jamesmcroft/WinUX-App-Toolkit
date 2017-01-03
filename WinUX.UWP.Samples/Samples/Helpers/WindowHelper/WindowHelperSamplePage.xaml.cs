namespace WinUX.UWP.Samples.Samples.Helpers.WindowHelper
{
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Navigation;

    using WinUX.Application.ViewManagement;
    using WinUX.UWP.Samples.Samples.Helpers.WindowHelper.Pages;
    using WinUX.UWP.Samples.ViewModels;

    public sealed partial class WindowHelperSamplePage
    {
        public WindowHelperSamplePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Fired when the page has been navigated to.
        /// </summary>
        /// <param name="e">
        /// The navigation parameters.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (AppShell.Instance != null && AppShell.Instance.ViewModel != null)
            {
                AppShell.Instance.ViewModel.Title = "WindowHelper sample";
            }

            var bindingSource = this.Sample.BindingSource;

            this.Properties.Visibility = bindingSource != null
                                             ? (bindingSource.Properties.Count > 0
                                                    ? Visibility.Visible
                                                    : Visibility.Collapsed)
                                             : Visibility.Collapsed;

            if (bindingSource != null)
            {
                this.Content.DataContext = bindingSource.Bindings;
            }
        }

        private async void OnSamplePageOneClicked(object sender, RoutedEventArgs e)
        {
            await WindowManager.CreateNewWindowForPageAsync(typeof(WindowHelperPageOne));
            await WindowManager.CreateNewWindowForPageAsync(typeof(WindowHelperPageOne), new Size(640, 480));
            await WindowManager.CreateNewWindowForPageAsync(typeof(WindowHelperPageOne), new MainPageViewModel());
            await WindowManager.CreateNewWindowForPageAsync(typeof(WindowHelperPageOne), new MainPageViewModel(), new Size(640, 480));

            // Also available with type name string.
            await WindowManager.CreateNewWindowForPageAsync("WinUX.Pages.WindowHelperPageOne, WinUX");
        }

        private async void OnSamplePageTwoClicked(object sender, RoutedEventArgs e)
        {
            await WindowManager.CreateNewWindowForPageAsync(typeof(WindowHelperPageTwo));
        }
    }
}