namespace WinUX.UWP.Samples
{
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;

    using Microsoft.Practices.ServiceLocation;

    using WinUX.Messaging.Dialogs;
    using WinUX.Mvvm.Services;
    using WinUX.Networking;
    using WinUX.UWP.Samples.Components;
    using WinUX.UWP.Samples.ViewModels;

    public sealed class Locator
    {
        static Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            RegisterServices();
            RegisterViewModels();
        }

        public AppShellViewModel AppShellViewModel => SimpleIoc.Default.GetInstance<AppShellViewModel>();

        public MainPageViewModel MainPageViewModel => SimpleIoc.Default.GetInstance<MainPageViewModel>();

        public SamplesPageViewModel SamplesPageViewModel => SimpleIoc.Default.GetInstance<SamplesPageViewModel>();

        private static void RegisterServices()
        {
            SimpleIoc.Default.Register<IMessenger, Messenger>();
            SimpleIoc.Default.Register(() => MessageDialogManager.Current);
            SimpleIoc.Default.Register(() => NetworkStatusManager.Current);
            SimpleIoc.Default.Register<INavigationService>(() => NavigationService.Current);
            SimpleIoc.Default.Register<SampleService>();
        }

        private static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<AppShellViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<SamplesPageViewModel>();
        }
    }
}