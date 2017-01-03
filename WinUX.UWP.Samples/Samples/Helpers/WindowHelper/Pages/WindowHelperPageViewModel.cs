namespace WinUX.UWP.Samples.Samples.Helpers.WindowHelper.Pages
{
    using Windows.UI.Xaml.Navigation;

    using WinUX.MvvmLight.Xaml.Views;

    public class WindowHelperPageViewModel : PageBaseViewModel
    {
        public override void OnPageNavigatedTo(NavigationEventArgs args)
        {
        }

        public override void OnPageNavigatedFrom(NavigationEventArgs args)
        {
        }

        public override void OnPageNavigatingFrom(NavigatingCancelEventArgs args)
        {
        }

        public void NavigateToPageOne()
        {
            this.NavigationService?.Navigate(typeof(WindowHelperPageOne));
        }

        public void NavigateToPageTwo()
        {
            this.NavigationService?.Navigate(typeof(WindowHelperPageTwo));
        }
    }
}