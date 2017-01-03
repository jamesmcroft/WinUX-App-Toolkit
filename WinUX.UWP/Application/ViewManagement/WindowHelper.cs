namespace WinUX.Application.ViewManagement
{
    using System;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Core;
    using Windows.Foundation;
    using Windows.UI.Core;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using WinUX.Diagnostics.Tracing;
    using WinUX.Messaging.Dialogs;
    using WinUX.Mvvm.Services;

    /// <summary>
    /// Defines helper methods for handling multiple windows of an application.
    /// </summary>
    public static class WindowHelper
    {
        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to load in the new Window.
        /// </param>
        /// <returns>
        /// Returns true if loaded; else false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(Type sourcePageType)
        {
            return await CreateNewWindowForPageAsync(sourcePageType, Size.Empty);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to load in the new Window.
        /// </param>
        /// <param name="desiredSize">
        /// The desired size of the new Window.
        /// </param>
        /// <returns>
        /// Returns true if loaded; else false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(Type sourcePageType, Size desiredSize)
        {
            return await CreateNewWindowForPageAsync(sourcePageType, null, desiredSize);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to load in the new Window.
        /// </param>
        /// <param name="parameter">
        /// The parameter to load in the new Window.
        /// </param>
        /// <returns>
        /// Returns true if loaded; else false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(Type sourcePageType, object parameter)
        {
            return await CreateNewWindowForPageAsync(sourcePageType, parameter, Size.Empty);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to load in the new Window.
        /// </param>
        /// <param name="parameter">
        /// The parameter to load in the new Window.
        /// </param>
        /// <param name="desiredSize">
        /// The desired size of the new Window.
        /// </param>
        /// <returns>
        /// Returns true if loaded; else false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(
            Type sourcePageType,
            object parameter,
            Size desiredSize)
        {
            var newApplicationView = CoreApplication.CreateNewView();

            var coreWindow = CoreApplication.GetCurrentView().CoreWindow;
            var mainViewId = ApplicationView.GetApplicationViewIdForWindow(coreWindow);

            var newApplicationViewId = 0;

            await newApplicationView.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        newApplicationViewId = ApplicationView.GetForCurrentView().Id;

                        var frame = new Frame();
                        Window.Current.Content = frame;

                        RegisterViewServices(newApplicationViewId, newApplicationView.Dispatcher, frame);

                        frame.Navigate(sourcePageType, parameter);
                        Window.Current.Activate();
                    });

            var shown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                            newApplicationViewId,
                            ViewSizePreference.UseMore,
                            mainViewId,
                            ViewSizePreference.Default);

            if (shown && desiredSize != Size.Empty)
            {
                await newApplicationView.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () =>
                        {
                            var view = ApplicationView.GetForCurrentView();
                            view?.TryResizeView(desiredSize);
                        });
            }

            return shown;
        }

        private static void RegisterViewServices(int viewId, CoreDispatcher viewDispatcher, Frame viewFrame)
        {
            try
            {
                ViewCoreDispatcherManager.Current.RegisterOrUpdate(viewId, viewDispatcher);
                ViewMessageDialogManager.Current.RegisterOrUpdate(viewId, new MessageDialogHelper(viewDispatcher));
                ViewNavigationServiceManager.Current.RegisterOrUpdate(viewId, new NavigationService(viewFrame));
            }
            catch (Exception ex)
            {
                EventLogger.Current.WriteError(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page name to load in the new Window.
        /// </param>
        /// <returns>
        /// Returns true if loaded; else false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(string sourcePageName)
        {
            return await CreateNewWindowForPageAsync(sourcePageName, Size.Empty);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page name to load in the new Window.
        /// </param>
        /// <param name="desiredSize">
        /// The desired size of the new Window.
        /// </param>
        /// <returns>
        /// Returns true if loaded; else false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(string sourcePageName, Size desiredSize)
        {
            return await CreateNewWindowForPageAsync(sourcePageName, null, desiredSize);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page name to load in the new Window.
        /// </param>
        /// <param name="parameter">
        /// The parameter to load in the new Window.
        /// </param>
        /// <returns>
        /// Returns true if loaded; else false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(string sourcePageName, object parameter)
        {
            return await CreateNewWindowForPageAsync(sourcePageName, parameter, Size.Empty);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page name to load in the new Window.
        /// </param>
        /// <param name="parameter">
        /// The parameter to load in the new Window.
        /// </param>
        /// <param name="desiredSize">
        /// The desired size of the new Window.
        /// </param>
        /// <returns>
        /// Returns true if loaded; else false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(
            string sourcePageName,
            object parameter,
            Size desiredSize)
        {
            var sourcePageType = Type.GetType(sourcePageName);
            if (sourcePageType != null) return await CreateNewWindowForPageAsync(sourcePageType, parameter, desiredSize);

            EventLogger.Current.WriteDebug(
                $"Couldn't create new Window for the type {sourcePageName} as it does not exist.");

            return false;
        }
    }
}