namespace WinUX.UWP.ApplicationModel.Lifecycle
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface for suspendable objects which can be managed by an <see cref="IAppLifecycleManager"/>.
    /// </summary>
    public interface ISuspendable
    {
        /// <summary>
        /// Suspends the object.
        /// </summary>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        Task SuspendAsync();

        /// <summary>
        /// Resumes the object.
        /// </summary>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        Task ResumeAsync();
    }
}