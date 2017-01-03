namespace WinUX.UWP.Samples.Samples.Controls.AppMenu
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Navigation;

    using WinUX.Mvvm.Services;
    using WinUX.UWP.Samples.Samples.Controls.AppMenu.Pages;

    public sealed partial class AppMenuSamplePage
    {
        public AppMenuSamplePage()
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
                AppShell.Instance.ViewModel.Title = "AppMenu sample";
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

            this.Menu.NavigationService = new NavigationService();
            this.Menu.NavigationService.Navigate(typeof(AppMenuPageOne));
        }

        /// <summary>
        /// Gets a value indicating whether the menu is grouped.
        /// </summary>
        public bool? IsMenuGrouped => true;
    }
}