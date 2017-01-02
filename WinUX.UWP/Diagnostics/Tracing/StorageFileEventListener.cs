namespace WinUX.UWP.Diagnostics.Tracing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Storage;

    /// <summary>
    /// Defines a helper for listening to changes to the event log and writing them out to a file.
    /// </summary>
    public sealed class StorageFileEventListener : EventListener
    {
        private const string Format = "{0:dd/MM/yyyy HH\\:mm\\:ss}\t '{1}'";

        private readonly SemaphoreSlim fileWriteSemaphore = new SemaphoreSlim(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFileEventListener"/> class.
        /// </summary>
        /// <param name="file">
        /// The file that contains the contents of the current log.
        /// </param>
        public StorageFileEventListener(StorageFile file)
        {
            this.File = file;
        }

        /// <summary>
        /// Gets the current log file.
        /// </summary>
        public StorageFile File { get; }

        /// <summary>
        /// Called whenever an event has been written by an event source for which the event listener has enabled events.
        /// </summary>
        /// <param name="eventData">
        /// The event arguments that describe the event.
        /// </param>
        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (this.File == null)
            {
                return;
            }

            this.Write(new[] { string.Format(Format, DateTime.Now, eventData.Payload[0]) });
        }

        private async void Write(IEnumerable<string> logs)
        {
            await this.fileWriteSemaphore.WaitAsync();

            await Task.Run(
                async () =>
                    {
                        try
                        {
                            await FileIO.AppendLinesAsync(this.File, logs);
                        }
                        catch (Exception ex)
                        {
                            EventLogger.Current.WriteError(
                                $"An exception was thrown while attempting to write to the log file. Error: '{ex.Message}'.");
                        }
                        finally
                        {
                            this.fileWriteSemaphore.Release();
                        }
                    });
        }
    }
}