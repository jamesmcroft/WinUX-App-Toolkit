namespace WinUX.Mvvm.Services
{
    using System;
    using System.Linq;

    using Windows.Foundation.Metadata;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Defines a basic navigation service handler for Windows applications.
    /// </summary>
    public class NavigationService : INavigationService
    {
        private readonly SystemNavigationManager navigationManager;

        private bool showTitleBarBackButton = true;

        private static NavigationService current;

        /// <summary>
        /// Gets the core application instance of the <see cref="NavigationService"/>.
        /// </summary>
        public static NavigationService Current => current ?? (current = new NavigationService(new Frame()));

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService"/> class.
        /// </summary>
        public NavigationService()
            : this(new Frame())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService"/> class.
        /// </summary>
        /// <param name="frame">
        /// The frame.
        /// </param>
        public NavigationService(Frame frame)
        {
            if (frame == null) throw new ArgumentNullException(nameof(frame));

            this.Frame = frame;
            this.Frame.Navigated += this.OnFrameNavigated;
            this.Frame.NavigationFailed += Frame_OnNavigationFailed;

            if (ApiInformation.IsTypePresent("Windows.UI.Core.SystemNavigationManager"))
            {
                this.navigationManager = SystemNavigationManager.GetForCurrentView();
                if (this.navigationManager != null)
                {
                    this.navigationManager.BackRequested += this.SystemNavigationManager_OnBackRequested;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the title bar back button for the core navigation service.
        /// </summary>
        /// <remarks>
        /// Changes to this property will only affect the static Current <see cref="NavigationService"/>.
        /// The default value is true.
        /// </remarks>
        public bool ShowTitleBarBackButton
        {
            get
            {
                return this.showTitleBarBackButton;
            }
            set
            {
                this.showTitleBarBackButton = value;
                this.UpdateBackButtonVisibility();
            }
        }

        private static void Frame_OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Failed to load Page {e.SourcePageType.FullName}.");
#endif
        }

        /// <summary>
        /// Gets the current page type.
        /// </summary>
        public Type CurrentPageType => this.Frame?.CurrentSourcePageType;

        /// <summary>
        /// Gets the current page's navigation parameter.
        /// </summary>
        public object CurrentPageNavigationParameter { get; private set; }

        /// <summary>
        /// Gets the navigation frame.
        /// </summary>
        public Frame Frame { get; }

        /// <summary>
        /// Gets a value indicating whether the frame can go back.
        /// </summary>
        public bool CanGoBack => this.Frame != null && this.Frame.CanGoBack;

        /// <summary>
        /// Navigates the current frame back.
        /// </summary>
        public void GoBack()
        {
            if (this.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        /// <summary>
        /// Navigates the current frame to the specified page type.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to navigate to.
        /// </param>
        /// <param name="transition">
        /// The navigation transition.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> indicating whether the navigation was successful.
        /// </returns>
        public bool Navigate(Type sourcePageType, NavigationTransitionInfo transition = null)
        {
            return this.Navigate(sourcePageType, null, transition);
        }

        /// <summary>
        /// Navigates the current frame to the specified page type with the specified parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to navigate to.
        /// </param>
        /// <param name="parameter">
        /// The parameter to pass to the page.
        /// </param>
        /// <param name="transition">
        /// The navigation transition.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> indicating whether the navigation was successful.
        /// </returns>
        public bool Navigate(Type sourcePageType, object parameter, NavigationTransitionInfo transition = null)
        {
            if (transition == null)
            {
                transition = new SlideNavigationTransitionInfo();
            }

            return this.Frame != null && this.Frame.Navigate(sourcePageType, parameter, transition);
        }

        /// <summary>
        /// Navigates the current frame to the specified page via the type's full name.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page type's full name to navigate to.
        /// </param>
        /// <param name="transition">
        /// The navigation transition.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> indicating whether the navigation was successful.
        /// </returns>
        public bool Navigate(string sourcePageName, NavigationTransitionInfo transition = null)
        {
            return this.Navigate(sourcePageName, null, transition);
        }

        /// <summary>
        /// Navigates the current frame to the specified page via the type's full name with the specified parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page type's full name to navigate to.
        /// </param>
        /// <param name="parameter">
        /// The parameter to pass to the page.
        /// </param>
        /// <param name="transition">
        /// The navigation transition.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> indicating whether the navigation was successful.
        /// </returns>
        public bool Navigate(string sourcePageName, object parameter, NavigationTransitionInfo transition = null)
        {
            var sourcePageType = Type.GetType(sourcePageName);
            if (sourcePageType != null) return this.Navigate(sourcePageType, parameter, transition);

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Couldn't navigate to the type {sourcePageName} as it does not exist.");
#endif

            return false;
        }

        /// <summary>
        /// Removes the last navigation entry in the backstack.
        /// </summary>
        public void RemoveLastNavigation()
        {
            var backStackIdx = this.Frame.BackStack.Count - 1;
            if (backStackIdx == -1) return;

            var removeItem = this.Frame.BackStack[backStackIdx];
            this.Frame.BackStack.Remove(removeItem);

            this.UpdateBackButtonVisibility();
        }

        /// <summary>
        /// Removes the last navigation entries in the backstack up to the specified page type.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to remove to.
        /// </param>
        public void RemoveToView(Type sourcePageType)
        {
            if (this.Frame != null)
            {
                for (var i = this.Frame.BackStack.Count - 1; i >= 0; i--)
                {
                    if (this.Frame.BackStack[i].SourcePageType == sourcePageType)
                    {
                        break;
                    }
                    this.Frame.BackStack.RemoveAt(i);
                }
            }

            this.UpdateBackButtonVisibility();
        }

        /// <summary>
        /// Removes the last navigation entries in the backstack up to the specified page via the type's full name.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page type's full name to remove to.
        /// </param>
        /// <example>
        /// this.NavigationService.RemoveToView("WinUX.UWP.MainPage, WinUX.UWP");
        /// </example>
        public void RemoveToView(string sourcePageName)
        {
            var sourcePageType = Type.GetType(sourcePageName);
            if (sourcePageType != null)
            {
                this.RemoveToView(sourcePageType);
            }
            else
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine( $"Couldn't remove navigation to the type {sourcePageName} as it does not exist.");
#endif

            }

            this.UpdateBackButtonVisibility();
        }

        /// <summary>
        /// Checks whether the navigation backstack entries contains the specified page type.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to find.
        /// </param>
        /// <returns>
        /// Returns true if the page exists; else false.
        /// </returns>
        public bool HasPreviouslyNavigatedTo(Type sourcePageType)
        {
            var frame = this.Frame?.BackStack.LastOrDefault(x => x.SourcePageType == sourcePageType);
            return frame != null;
        }

        /// <summary>
        /// Checks whether the navigation backstack entries contains the specified page via the type's full name.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page type's full name to find.
        /// </param>
        /// <returns>
        /// Returns true if the page exists; else false.
        /// </returns>
        public bool HasPreviouslyNavigatedTo(string sourcePageName)
        {
            var sourcePageType = Type.GetType(sourcePageName);
            if (sourcePageType != null) return this.HasPreviouslyNavigatedTo(sourcePageType);

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Couldn't check if previously navigated to the type {sourcePageName} as it does not exist.");
#endif

            return false;
        }

        /// <summary>
        /// Clears all navigation backstack entries up to the current page.
        /// </summary>
        public void ClearNavigationHistory()
        {
            this.Frame?.BackStack.Clear();

            this.UpdateBackButtonVisibility();
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs args)
        {
            this.CurrentPageNavigationParameter = args.Parameter;

            this.UpdateBackButtonVisibility();
        }

        private void UpdateBackButtonVisibility()
        {
            // This code allows us to use the Windows back button rather than an in-app one
            if (this.ShowTitleBarBackButton)
            {
                if (this.CanGoBack)
                {
                    if (this.navigationManager != null)
                    {
                        this.navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                }
                else
                {
                    if (this.navigationManager != null)
                    {
                        this.navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                }
            }
            else
            {
                if (this.navigationManager != null
                    && this.navigationManager.AppViewBackButtonVisibility == AppViewBackButtonVisibility.Visible)
                {
                    this.navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                }
            }
        }

        private void SystemNavigationManager_OnBackRequested(object sender, BackRequestedEventArgs args)
        {
            if (this.CanGoBack)
            {
                args.Handled = true;
                this.GoBack();
            }
        }
    }
}