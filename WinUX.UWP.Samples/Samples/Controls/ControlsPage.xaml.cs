namespace WinUX.UWP.Samples.Samples.Controls
{
    using WinUX.UWP.Samples.ViewModels;

    public sealed partial class ControlsPage
    {
        public ControlsPage()
        {
            this.InitializeComponent();
        }

        public SamplesPageViewModel ViewModel => this.DataContext as SamplesPageViewModel;
    }
}