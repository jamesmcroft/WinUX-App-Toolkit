namespace WinUX.UWP.ApplicationModel.Lifecycle
{
    using System.Threading.Tasks;

    using Windows.ApplicationModel;

    /// <summary>
    /// Defines an interface for an application lifecycle manager.
    /// </summary>
    public interface IAppLifecycleManager
    {
        /// <summary>
        /// Adds an item to the manager.
        /// </summary>
        /// <param name="suspendable">
        /// The suspendable item to add.
        /// </param>
        void Add(ISuspendable suspendable);

        /// <summary>
        /// Removes an item from the manager.
        /// </summary>
        /// <param name="suspendable">
        /// The suspendable item to remove.
        /// </param>
        void Remove(ISuspendable suspendable);

        /// <summary>
        /// Suspends all registered suspendable items.
        /// </summary>
        /// <remarks>
        /// This should be called when the Suspending event handler is invoked from the App.xaml.cs.
        /// </remarks>
        /// <param name="args">
        /// The application's suspension arguments.
        /// </param>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        Task SuspendAsync(SuspendingEventArgs args);

        /// <summary>
        /// Resumes all registered suspendable items.
        /// </summary>
        /// <remarks>
        /// This should be called when the Resuming event handler is invoked from the App.xaml.cs.
        /// </remarks>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        Task ResumeAsync();
    }
}