namespace WinUX.UWP.Samples.Samples.Helpers.AppDiagnostics
{
    using System;

    using Windows.System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Navigation;

    using WinUX.Controls.ScrollViewer;
    using WinUX.Diagnostics;
    using WinUX.Diagnostics.Tracing;

    public sealed partial class AppDiagnosticsSamplePage
    {
        public AppDiagnosticsSamplePage()
        {
            this.InitializeComponent();
            this.ScrollViewer.UpdateScrollMode(ScrollViewerMode.Vertical);
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
                AppShell.Instance.ViewModel.Title = "AppDiagnostics sample";
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

        private async void OnOpenAppLogClicked(object sender, RoutedEventArgs e)
        {
            if (AppDiagnostics.Current.DiagnosticsFile != null)
            {
                await Launcher.LaunchFileAsync(AppDiagnostics.Current.DiagnosticsFile);
            }
        }

        private void OnLogDebugClicked(object sender, RoutedEventArgs e)
        {
            EventLogger.Current.WriteDebug("This is a debug event.");
        }

        private void OnLogInfoClicked(object sender, RoutedEventArgs e)
        {
            EventLogger.Current.WriteInfo("This is an info event.");
        }

        private void OnLogWarningClicked(object sender, RoutedEventArgs e)
        {
            EventLogger.Current.WriteWarning("This is a warning event.");
        }

        private void OnLogErrorClicked(object sender, RoutedEventArgs e)
        {
            EventLogger.Current.WriteError("This is an error event.");
        }

        private void OnLogCriticalClicked(object sender, RoutedEventArgs e)
        {
            EventLogger.Current.WriteCritical("This is a critical event.");
        }

        private void OnThrowExceptionClicked(object sender, RoutedEventArgs e)
        {
            throw new Exception("This is an exception that has been thrown without a try/catch.");
        }
    }
}