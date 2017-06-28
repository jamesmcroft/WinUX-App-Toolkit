namespace WinUX.Diagnostics.Tracing
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using XPlat.Storage;

    /// <summary>
    /// Defines an event logger for logging information to a file for an Android application.
    /// </summary>
    public class EventLogger : IEventLogger
    {
        private const string Format = "{0:dd/MM/yyyy HH\\:mm\\:ss}\t '{1}': {2}";

        private readonly SemaphoreSlim fileWriteSemaphore = new SemaphoreSlim(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLogger"/> class.
        /// </summary>
        /// <remarks>
        /// This will create or open the file for event logging.
        /// </remarks>
        public EventLogger()
        {
            this.InitializeLogFile();
        }

        /// <summary>
        /// Gets the storage file that is used to store the application log.
        /// </summary>
        public IStorageFile File { get; private set; }

        /// <inheritdoc />
        public async void WriteDebug(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);

            var line = string.Format(Format, DateTime.Now, "Debug", message);
            await this.WriteEventAsync(line);
#endif
        }

        /// <inheritdoc />
        public async void WriteInfo(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            var line = string.Format(Format, DateTime.Now, "Info", message);
            await this.WriteEventAsync(line);
        }

        /// <inheritdoc />
        public async void WriteWarning(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            var line = string.Format(Format, DateTime.Now, "Warning", message);
            await this.WriteEventAsync(line);
        }

        /// <inheritdoc />
        public async void WriteError(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            var line = string.Format(Format, DateTime.Now, "Error", message);
            await this.WriteEventAsync(line);
        }

        /// <inheritdoc />
        public async void WriteCritical(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            var line = string.Format(Format, DateTime.Now, "Critical", message);
            await this.WriteEventAsync(line);
        }

        private void InitializeLogFile()
        {
            var task = ApplicationData.Current.LocalFolder.CreateFileAsync(
                $"EventLogger-{DateTime.Now:yyyyMMdd}.txt",
                CreationCollisionOption.OpenIfExists);

            task.Wait();

            this.File = task.Result;
        }

        private async Task WriteEventAsync(string line)
        {
            if (this.File != null)
            {
                await this.fileWriteSemaphore.WaitAsync();

                try
                {
                    using (StreamWriter writer = System.IO.File.AppendText(this.File.Path))
                    {
                        writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(
                        $"An exception was thrown while attempting to write to the log file. Error: '{ex}'.");
#endif
                }
                finally
                {
                    this.fileWriteSemaphore.Release();
                }
            }
        }
    }
}