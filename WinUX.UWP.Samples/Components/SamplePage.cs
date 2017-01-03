namespace WinUX.UWP.Samples.Components
{
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using WinUX.Mvvm.Services;

    public abstract class SamplePage : Page
    {
        /// <summary>
        /// Fired when the page has been navigated to.
        /// </summary>
        /// <param name="e">
        /// The navigation parameters.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var sample = e.Parameter as Sample;
            if (sample == null)
            {
                NavigationService.Current.GoBack();
                return;
            }

            var propertyDescriptor = sample.BindingSource;
            if (propertyDescriptor == null)
            {
                NavigationService.Current.GoBack();
            }

            this.DataContext = sample;
        }

        /// <summary>
        /// Gets the sample model for this page.
        /// </summary>
        public Sample Sample => this.DataContext as Sample;
    }
}