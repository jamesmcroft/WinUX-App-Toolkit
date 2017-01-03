namespace WinUX.UWP.Samples
{
    using WinUX.Mvvm.Services;
    using WinUX.UWP.Samples.ViewModels;
    using WinUX.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell
    {
        public AppShell()
        {
            Instance = this;
            this.InitializeComponent();

            this.AppMenu.NavigationService = NavigationService.Current;
        }

        /// <summary>
        /// Gets the instance of the shell.
        /// </summary>
        public static AppShell Instance { get; private set; }

        /// <summary>
        /// Gets the app menu.
        /// </summary>
        public static AppMenu Menu => Instance.AppMenu;

        /// <summary>
        /// Gets the data context for the app shell.
        /// </summary>
        public AppShellViewModel ViewModel => this.DataContext as AppShellViewModel;
    }
}