namespace WinUX.Diagnostics
{
    using System;
    using System.Diagnostics.Tracing;
    using System.Threading.Tasks;

    using Windows.Storage;
    using Windows.UI.Xaml;

    using WinUX.Diagnostics.Tracing;

    /// <summary>
    /// Defines a helper for automatically handling any unhandled application errors and logging them to a file.
    /// </summary>
    public class AppDiagnostics : IAppDiagnostics
    {
        private StorageFileEventListener listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDiagnostics"/> class.
        /// </summary>
        /// <param name="eventLogger">
        /// An instance of an <see cref="IEventLogger"/>.
        /// </param>
        public AppDiagnostics(IEventLogger eventLogger)
        {
            this.EventLogger = eventLogger;
        }

        /// <inheritdoc />
        public IEventLogger EventLogger { get; }

        /// <inheritdoc />
        public StorageFile DiagnosticsFile { get; private set; }

        /// <inheritdoc />
        public bool IsRecording { get; private set; }

        /// <inheritdoc />
        public async Task StartAsync()
        {
            if (Application.Current == null || this.IsRecording)
            {
                return;
            }

            await this.SetupEventListener();

            Application.Current.UnhandledException += this.OnAppUnhandledExceptionThrown;
            TaskScheduler.UnobservedTaskException += this.OnAppUnobservedTaskExceptionThrown;

            this.IsRecording = true;
        }

        /// <inheritdoc />
        public void Stop()
        {
            if (Application.Current == null || !this.IsRecording)
            {
                return;
            }

            Application.Current.UnhandledException -= this.OnAppUnhandledExceptionThrown;
            TaskScheduler.UnobservedTaskException -= this.OnAppUnobservedTaskExceptionThrown;

            this.IsRecording = false;
        }

        private void OnAppUnobservedTaskExceptionThrown(object sender, UnobservedTaskExceptionEventArgs args)
        {
            args.SetObserved();

            this.EventLogger.WriteCritical(
                args.Exception != null
                    ? $"Unobserved task exception thrown. Error: '{args.Exception}'"
                    : "Unobserved task exception thrown. Error: 'No exception information available.'");
        }

        private void OnAppUnhandledExceptionThrown(object sender, UnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            this.EventLogger.WriteCritical($"Unhandled exception thrown. Error: '{args.Message}'");
        }

        private async Task SetupEventListener()
        {
            this.DiagnosticsFile =
                await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    $"EventLogger-{DateTime.Now:yyyyMMdd}.txt",
                    CreationCollisionOption.OpenIfExists);

            this.listener = new StorageFileEventListener(this.DiagnosticsFile);
            this.listener.EnableEvents(this.EventLogger as EventSource, EventLevel.Verbose);

            this.EventLogger.WriteInfo("Application diagnostics initialized.");
        }
    }
}