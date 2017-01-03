namespace WinUX.Diagnostics
{
    using System.Threading.Tasks;

    using Windows.Storage;

    public interface IAppDiagnostics
    {
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
