namespace WinUX.Diagnostics
{
    using System.Threading.Tasks;

    using Windows.Storage;

    using WinUX.Diagnostics.Tracing;

    /// <summary>
    /// Defines an interface for an app diagnostics provider.
    /// </summary>
    public interface IAppDiagnostics
    {
        /// <summary>
        /// Gets an instance of the event logger.
        /// </summary>
        IEventLogger EventLogger { get; }

        /// <summary>
        /// Gets the file used for application diagnostic messages.
        /// </summary>
        StorageFile DiagnosticsFile { get; }

        /// <summary>
        /// Gets a value indicating whether diagnostics are being recorded.
        /// </summary>
        bool IsRecording { get; }

        /// <summary>
        /// Starts the diagnostics tracking.
        /// </summary>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        Task StartAsync();

        /// <summary>
        /// Stops the diagnostics tracking.
        /// </summary>
        void Stop();
    }
}
