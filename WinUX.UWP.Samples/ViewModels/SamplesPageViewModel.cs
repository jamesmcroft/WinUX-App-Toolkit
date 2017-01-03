namespace WinUX.UWP.Samples.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Windows.UI.Xaml.Navigation;

    using GalaSoft.MvvmLight.Command;

    using WinUX.UWP.Samples.Components;

    public sealed class SamplesPageViewModel : SamplePageBaseViewModel
    {
        public SamplesPageViewModel()
        {
            this.Samples = new ObservableCollection<Sample>();
            this.SampleItemClickedCommand = new RelayCommand<Sample>(NavigateToSample);
        }

        public ObservableCollection<Sample> Samples { get; }

        public ICommand SampleItemClickedCommand { get; }

        public override void OnPageNavigatedTo(NavigationEventArgs args)
        {
            this.Samples.Clear();

            var collection = args.Parameter as SampleCollection;
            if (collection == null) return;

            this.AppShell.Title = string.IsNullOrWhiteSpace(collection.Name) ? "Samples" : collection.Name;

            if (collection.Samples != null)
            {
                this.Samples.AddRange(collection.Samples);
            }
        }

        public override void OnPageNavigatedFrom(NavigationEventArgs args)
        {
        }

        public override void OnPageNavigatingFrom(NavigatingCancelEventArgs args)
        {
            this.Samples.Clear();
        }

        private static void NavigateToSample(Sample sample)
        {
            sample?.NavigateTo();
        }
    }
}