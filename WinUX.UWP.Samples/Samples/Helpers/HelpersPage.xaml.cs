namespace WinUX.UWP.Samples.Samples.Helpers
{
    using WinUX.UWP.Samples.ViewModels;

    public sealed partial class HelpersPage
    {
        public HelpersPage()
        {
            this.InitializeComponent();
        }

        public SamplesPageViewModel ViewModel => this.DataContext as SamplesPageViewModel;
    }
}