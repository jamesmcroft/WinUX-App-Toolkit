namespace WinUX.Xaml
{
    using System;
    using System.Threading.Tasks;

    using Windows.UI.Core;
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines a helper for running actions on the app window UI thread.
    /// </summary>
    /// <remarks>
    /// Only use this class in single window applications. 
    /// If creating a multi-window experience, consider using WinUX.Application.ViewManagement.ViewCoreDispatcherManager which is part of this package.
    /// </remarks>
    public static class UIDispatcher
    {
        /// <summary>
        /// Gets the instance of the dispatcher.
        /// </summary>
        internal static CoreDispatcher Instance { get; private set; }

        private static bool isInitialized;

        /// <summary>
        /// Initializes the UI dispatcher.
        /// </summary>
        /// <remarks>
        /// This should be called from the OnLaunched event of the App.xaml.cs.
        /// </remarks>
        public static void Initialize()
        {
            SetDispatcherInstance();
            isInitialized = true;
        }

        /// <summary>
        /// Runs the action on the UI thread.
        /// </summary>
        /// <param name="action">
        /// The action to run.
        /// </param>
        public static async void Run(Action action)
        {
            if (!isInitialized)
            {
                throw new InvalidOperationException(
                          "An action cannot be run on the UIDispatcher as it has not been initialized.");
            }

            if (action == null) return;

            await Instance.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }

        /// <summary>
        /// Runs the asynchronous action on the UI thread.
        /// </summary>
        /// <param name="action">
        /// The asynchronous action to run.
        /// </param>
        public static async void Run(Func<Task> action)
        {
            if (!isInitialized)
            {
                throw new InvalidOperationException(
                          "An action cannot be run on the UIDispatcher as it has not been initialized.");
            }

            if (action == null) return;

            await Instance.RunAsync(CoreDispatcherPriority.Normal, async () => await action());
        }

        /// <summary>
        /// Runs the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">
        /// The action to run.
        /// </param>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        public static async Task RunAsync(Action action)
        {
            if (!isInitialized)
            {
                throw new InvalidOperationException(
                          "An action cannot be run on the UIDispatcher as it has not been initialized.");
            }

            if (action == null) return;

            await Instance.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }

        /// <summary>
        /// Runs the asynchronous action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">
        /// The asynchronous action to run.
        /// </param>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        public static async Task RunAsync(Func<Task> action)
        {
            if (!isInitialized)
            {
                throw new InvalidOperationException(
                          "An action cannot be run on the UIDispatcher as it has not been initialized.");
            }

            if (action == null) return;

            await Instance.RunAsync(CoreDispatcherPriority.Normal, async () => await action());
        }

        private static void SetDispatcherInstance()
        {
            if (!isInitialized && Instance == null)
            {
                Instance = Window.Current.Dispatcher;
            }
        }
    }
}