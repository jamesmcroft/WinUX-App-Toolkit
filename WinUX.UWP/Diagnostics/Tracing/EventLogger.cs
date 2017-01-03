namespace WinUX.Diagnostics.Tracing
{
    using System.Diagnostics.Tracing;

    /// <summary>
    /// Defines an event logger for Windows event tracing.
    /// </summary>
    public sealed class EventLogger : EventSource
    {
        /// <summary>
        /// Gets an instance of the event logger.
        /// </summary>
        public static readonly EventLogger Current = new EventLogger();

        /// <summary>
        /// Writes a debug information message to the event log.
        /// </summary>
        /// <param name="message">
        /// The debug information message to write out.
        /// </param>
        /// <remarks>
        /// This will only write out if the application is running in debug mode.
        /// </remarks>
        [Event(1, Message = "Debug: {0}", Level = EventLevel.Informational)]
        public void WriteDebug(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
            this.WriteEvent(1, message);
#endif
        }

        /// <summary>
        /// Writes a generic information message to the event log.
        /// </summary>
        /// <param name="message">
        /// The generic information message to write out.
        /// </param>
        [Event(2, Message = "Info: {0}", Level = EventLevel.Informational)]
        public void WriteInfo(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            this.WriteEvent(2, message);
        }

        /// <summary>
        /// Writes a warning message to the event log.
        /// </summary>
        /// <param name="message">
        /// The warning message to write out.
        /// </param>        
        [Event(3, Message = "Warning: {0}", Level = EventLevel.Warning)]
        public void WriteWarning(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            this.WriteEvent(3, message);
        }

        /// <summary>
        /// Writes an error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The error message to write out.
        /// </param>
        [Event(4, Message = "Error: {0}", Level = EventLevel.Error)]
        public void WriteError(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            this.WriteEvent(4, message);
        }

        /// <summary>
        /// Writes a critical message to the event log.
        /// </summary>
        /// <param name="message">
        /// The critical message to write out.
        /// </param>
        [Event(5, Message = "Critical: {0}", Level = EventLevel.Critical)]
        public void WriteCritical(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            this.WriteEvent(5, message);
        }
    }
}