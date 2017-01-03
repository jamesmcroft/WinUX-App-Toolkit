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
        private static AppDiagnostics current;

        /// <summary>
        /// Gets an instance of the <see cref="AppDiagnostics"/>.
        /// </summary>
        public static AppDiagnostics Current => current ?? (current = new AppDiagnostics());

        private StorageFileEventListener listener;

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

            Application.Current.UnhandledException += OnAppUnhandledExceptionThrown;
            TaskScheduler.UnobservedTaskException += OnAppUnobservedTaskExceptionThrown;

            this.IsRecording = true;
        }

        /// <inheritdoc />
        public void Stop()
        {
            if (Application.Current == null || !this.IsRecording)
            {
                return;
            }

            Application.Current.UnhandledException -= OnAppUnhandledExceptionThrown;
            TaskScheduler.UnobservedTaskException -= OnAppUnobservedTaskExceptionThrown;

            this.IsRecording = false;
        }

        private static void OnAppUnobservedTaskExceptionThrown(object sender, UnobservedTaskExceptionEventArgs args)
        {
            args.SetObserved();

            if (args.Exception != null)
            {
                EventLogger.Current.WriteCritical($"Unobserved task exception thrown. Error: '{args.Exception}'");

                if (!string.IsNullOrWhiteSpace(args.Exception.StackTrace))
                {
                    EventLogger.Current.WriteInfo($"StackTrace: {args.Exception.StackTrace}");
                }
            }
            else
            {
                EventLogger.Current.WriteCritical(
                    "Unobserved task exception thrown. No exception information available.");
            }
        }

        private static void OnAppUnhandledExceptionThrown(object sender, UnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            EventLogger.Current.WriteCritical($"Unhandled exception thrown. Error: '{args.Message}'");
        }

        private async Task SetupEventListener()
        {
            this.DiagnosticsFile =
                await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    $"diag-{DateTime.Now:dd-MM-yyyy}.txt",
                    CreationCollisionOption.OpenIfExists);

            this.listener = new StorageFileEventListener(this.DiagnosticsFile);
            this.listener.EnableEvents(EventLogger.Current, EventLevel.Verbose);

            EventLogger.Current.WriteInfo("Application diagnostics initialized.");
        }
    }
}