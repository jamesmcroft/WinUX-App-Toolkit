namespace WinUX.MvvmLight.Xaml.Views
{
    using System;

    using Windows.System.Profile;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using WinUX.Device.Profile;
    using WinUX.Xaml;

    /// <summary>
    /// Defines an MvvmLight friendly base page.
    /// </summary>
    public abstract class PageBase : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageBase"/> class.
        /// </summary>
        protected PageBase()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }

            this.Loaded += this.OnPageLoaded;
            this.SizeChanged += this.OnPageSizeChanged;

            this.RegisterForView();
        }

        private void RegisterForView()
        {
            try
            {
                var view = ApplicationView.GetForCurrentView();
                this.ViewId = view?.Id ?? -1;
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }
        }

        /// <summary>
        /// Gets the device type that the application is currently running on.
        /// </summary>
        public DeviceType DeviceType => AnalyticsInfo.VersionInfo.GetDeviceType();

        /// <summary>
        /// Gets the identifier for the application view this page was created for.
        /// </summary>
        public int ViewId { get; private set; }

        /// <summary>
        /// Gets the width for the narrow adaptive state.
        /// </summary>
        /// <remarks>
        /// The narrow state is set to 0.
        /// </remarks>
        public double NarrowAdaptiveWidth => 0;

        /// <summary>
        /// Gets or sets the width for the normal adaptive state.
        /// </summary>
        /// <remarks>
        /// The normal state defaults to 720.
        /// </remarks>
        public double NormalAdaptiveWidth { get; set; } = 720;

        /// <summary>
        /// Gets or sets the width for the wide adaptive state.
        /// </summary>
        /// <remarks>
        /// The wide state defaults to 1920.
        /// </remarks>
        public double WideAdaptiveWidth { get; set; } = 1920;

        /// <summary>
        /// Called when the page has been navigated to.
        /// </summary>
        /// <param name="e">
        /// The navigation arguments.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }

            var vm = this.DataContext as PageBaseViewModel;
            vm?.OnPageNavigatedTo(e);
        }

        /// <summary>
        /// Called when the page is navigating to another.
        /// </summary>
        /// <param name="e">
        /// The navigation cancellation arguments.
        /// </param>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }

            var vm = this.DataContext as PageBaseViewModel;
            vm?.OnPageNavigatingFrom(e);
        }

        /// <summary>
        /// Called when the page has been navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation arguments.
        /// </param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }

            var vm = this.DataContext as PageBaseViewModel;
            vm?.OnPageNavigatedFrom(e);
        }

        private void OnPageLoaded(object sender, RoutedEventArgs args)
        {
            var vm = this.DataContext as PageBaseViewModel;
            vm?.OnPageLoaded();
        }

        private void OnPageSizeChanged(object sender, SizeChangedEventArgs args)
        {
            var vm = this.DataContext as PageBaseViewModel;
            vm?.OnPageSizeChanged(args.PreviousSize, args.NewSize);

            this.UpdatePageAdpativeState(args);
        }

        private void UpdatePageAdpativeState(SizeChangedEventArgs args)
        {
            var vm = this.DataContext as PageBaseViewModel;

            if (args.PreviousSize.Width >= 0 && args.PreviousSize.Width < this.NormalAdaptiveWidth)
            {
                // Previous state was narrow
                if (args.NewSize.Width > this.NormalAdaptiveWidth && args.NewSize.Width <= this.WideAdaptiveWidth)
                {
                    // New state is normal
                    vm?.OnPageAdaptiveStateChanged(AdaptiveState.Narrow, AdaptiveState.Normal);
                }
                else if (args.NewSize.Width > this.WideAdaptiveWidth)
                {
                    // New state is wide
                    vm?.OnPageAdaptiveStateChanged(AdaptiveState.Narrow, AdaptiveState.Wide);
                }
            }
            else if (args.PreviousSize.Width > this.NormalAdaptiveWidth && args.PreviousSize.Width <= this.WideAdaptiveWidth)
            {
                // Previous state was normal
                if (args.NewSize.Width >= 0 && args.NewSize.Width < this.NormalAdaptiveWidth)
                {
                    // New state is narrow
                    vm?.OnPageAdaptiveStateChanged(AdaptiveState.Normal, AdaptiveState.Narrow);
                }
                else if (args.NewSize.Width > this.WideAdaptiveWidth)
                {
                    // New state is wide
                    vm?.OnPageAdaptiveStateChanged(AdaptiveState.Normal, AdaptiveState.Wide);
                }
            }
            else if (args.PreviousSize.Width > this.WideAdaptiveWidth)
            {
                // Previous state was wide
                if (args.NewSize.Width >= 0 && args.NewSize.Width < this.NormalAdaptiveWidth)
                {
                    // New state is narrow
                    vm?.OnPageAdaptiveStateChanged(AdaptiveState.Wide, AdaptiveState.Narrow);
                }
                else if (args.NewSize.Width > this.NormalAdaptiveWidth && args.NewSize.Width <= this.WideAdaptiveWidth)
                {
                    // New state is normal
                    vm?.OnPageAdaptiveStateChanged(AdaptiveState.Wide, AdaptiveState.Normal);
                }
            }
        }
    }
}