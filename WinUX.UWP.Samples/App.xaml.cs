namespace WinUX.UWP.Samples
{
    using System;
    using System.Threading.Tasks;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.Foundation.Metadata;
    using Windows.UI;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using GalaSoft.MvvmLight.Ioc;

    using WinUX.ApplicationModel.Lifecycle;
    using WinUX.Design.Material;
    using WinUX.Diagnostics;
    using WinUX.Diagnostics.Tracing;
    using WinUX.Mvvm.Services;
    using WinUX.UWP.Samples.ViewModels;
    using WinUX.Xaml;

    public sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += OnResuming;
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            UpdateTitleBar();

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;

                await AppDiagnostics.Current.StartAsync();
            }

            UIDispatcher.Initialize();

            if (e.PrelaunchActivated) return;

            if (rootFrame.Content == null)
            {
                await InitializeAppShellAsync(e.Arguments);
                rootFrame.Navigate(typeof(AppShell));
            }

            Window.Current.Activate();
        }

        private static async Task InitializeAppShellAsync(object navigationParameter)
        {
            try
            {
                var shellVm = SimpleIoc.Default.GetInstance<AppShellViewModel>();
                if (shellVm != null)
                {
                    await shellVm.InitializeAsync();
                }
            }
            catch (Exception ex)
            {
                EventLogger.Current.WriteError(ex.Message);
            }

            NavigationService.Current.Navigate(typeof(MainPage), navigationParameter);
        }

        private static void UpdateTitleBar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ForegroundColor = Colors.White;
                    titleBar.BackgroundColor = MaterialDesignColors.Blue.Color700;

                    titleBar.ButtonInactiveForegroundColor = Colors.Black;
                    titleBar.ButtonInactiveBackgroundColor = MaterialDesignColors.Blue.Color50;

                    titleBar.InactiveForegroundColor = Colors.Black;
                    titleBar.InactiveBackgroundColor = MaterialDesignColors.Blue.Color100;

                    titleBar.ButtonHoverForegroundColor = Colors.Black;
                    titleBar.ButtonHoverBackgroundColor = MaterialDesignColors.Blue.Color400;

                    titleBar.ButtonForegroundColor = Colors.White;
                    titleBar.ButtonBackgroundColor = MaterialDesignColors.Blue.Color800;

                    titleBar.ButtonPressedForegroundColor = Colors.White;
                    titleBar.ButtonPressedBackgroundColor = MaterialDesignColors.Blue.Color900;
                }
            }

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = MaterialDesignColors.Blue.Color700;
                    statusBar.ForegroundColor = Colors.White;
                }
            }
        }

        private static void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private static void OnSuspending(object sender, SuspendingEventArgs e)
        {
            AppLifecycleManager.Current.SuspendAsync(e);
        }

        private static void OnResuming(object sender, object e)
        {
            AppLifecycleManager.Current.ResumeAsync();
        }
    }
}