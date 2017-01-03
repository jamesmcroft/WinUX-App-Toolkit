namespace WinUX.UWP.Samples.Samples.Design
{
    using WinUX.UWP.Samples.ViewModels;

    public sealed partial class DesignPage
    {
        public DesignPage()
        {
            this.InitializeComponent();
        }

        public SamplesPageViewModel ViewModel => this.DataContext as SamplesPageViewModel;
    }
}