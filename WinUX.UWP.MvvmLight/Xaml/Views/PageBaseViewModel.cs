namespace WinUX.MvvmLight.Xaml.Views
{
    using Windows.Foundation;
    using Windows.UI.Xaml.Navigation;

    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;

    using WinUX.MvvmLight.Common.ViewModels;
    using WinUX.Xaml;

    /// <summary>
    /// Defines a core base view model for the <see cref="PageBase"/> which contains common page logic.
    /// </summary>
    public abstract class PageBaseViewModel : CoreViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageBaseViewModel"/> class.
        /// </summary>
        protected PageBaseViewModel()
            : this(SimpleIoc.Default.GetInstance<IMessenger>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageBaseViewModel"/> class.
        /// </summary>
        /// <param name="messenger">
        /// The MvvmLight IMessenger.
        /// </param>
        [PreferredConstructor]
        protected PageBaseViewModel(IMessenger messenger)
            : base(messenger)
        {
        }

        /// <summary>
        /// Called when the page has loaded.
        /// </summary>
        public virtual void OnPageLoaded()
        {
        }

        /// <summary>
        /// Called when the page size changes.
        /// </summary>
        /// <param name="previous">
        /// The previous size.
        /// </param>
        /// <param name="updated">
        /// The size updated to.
        /// </param>
        public virtual void OnPageSizeChanged(Size previous, Size updated)
        {
        }

        /// <summary>
        /// Called when the adaptive state of the page changes.
        /// </summary>
        /// <param name="previous">
        /// The previous adaptive state.
        /// </param>
        /// <param name="updated">
        /// The adaptive state updated to.
        /// </param>
        public virtual void OnPageAdaptiveStateChanged(AdaptiveState previous, AdaptiveState updated)
        {
        }

        /// <summary>
        /// Called when the page has been navigated to.
        /// </summary>
        /// <param name="args">
        /// The navigation arguments.
        /// </param>
        public abstract void OnPageNavigatedTo(NavigationEventArgs args);

        /// <summary>
        /// Called when the page has been navigated from.
        /// </summary>
        /// <param name="args">
        /// The navigation arguments.
        /// </param>
        public abstract void OnPageNavigatedFrom(NavigationEventArgs args);

        /// <summary>
        /// Called when the page is navigating to another.
        /// </summary>
        /// <param name="args">
        /// The navigation cancellation arguments.
        /// </param>
        public abstract void OnPageNavigatingFrom(NavigatingCancelEventArgs args);
    }
}