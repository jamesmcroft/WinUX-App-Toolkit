namespace WinUX.UWP.Samples
{
    using WinUX.UWP.Samples.ViewModels;

    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public MainPageViewModel ViewModel => this.DataContext as MainPageViewModel;
    }
}