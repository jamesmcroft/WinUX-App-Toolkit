namespace WinUX.UWP.Samples.ViewModels
{
    using Windows.ApplicationModel;
    using Windows.UI.Xaml.Navigation;

    public sealed class MainPageViewModel : SamplePageBaseViewModel
    {
        /// <summary>
        /// Gets the application's package version number.
        /// </summary>
        public string VersionNumber
            =>
            $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}"
            ;

        public override void OnPageNavigatedTo(NavigationEventArgs args)
        {
            this.AppShell.Title = "WinUX Samples";
        }

        public override void OnPageNavigatedFrom(NavigationEventArgs args)
        {
        }

        public override void OnPageNavigatingFrom(NavigatingCancelEventArgs args)
        {
        }
    }
}