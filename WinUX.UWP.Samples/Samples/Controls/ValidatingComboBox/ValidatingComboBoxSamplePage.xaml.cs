namespace WinUX.UWP.Samples.Samples.Controls.ValidatingComboBox
{
    using System.Collections.Generic;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class ValidatingComboBoxSamplePage
    {
        public ValidatingComboBoxSamplePage()
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
                AppShell.Instance.ViewModel.Title = "ValidatingComboBox sample";
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

            this.ValidatingComboBox.ItemsSource = new List<string>
                                                      {
                                                          " ",
                                                          "Hello, World!",
                                                          "This is a ValidatingComboBox"
                                                      };
        }
    }
}