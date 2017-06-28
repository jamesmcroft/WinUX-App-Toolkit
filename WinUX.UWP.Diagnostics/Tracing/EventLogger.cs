namespace WinUX.Diagnostics.Tracing
{
    using System.Diagnostics.Tracing;

    /// <summary>
    /// Defines an event logger for logging information to a file for a UWP application.
    /// </summary>
    public sealed class EventLogger : EventSource, IEventLogger
    {
        /// <inheritdoc />
        [Event(1, Message = "Debug: {0}", Level = EventLevel.Informational)]
        public void WriteDebug(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
            this.WriteEvent(1, message);
#endif
        }

        /// <inheritdoc />
        [Event(2, Message = "Info: {0}", Level = EventLevel.Informational)]
        public void WriteInfo(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            this.WriteEvent(2, message);
        }

        /// <inheritdoc />
        [Event(3, Message = "Warning: {0}", Level = EventLevel.Warning)]
        public void WriteWarning(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            this.WriteEvent(3, message);
        }

        /// <inheritdoc />
        [Event(4, Message = "Error: {0}", Level = EventLevel.Error)]
        public void WriteError(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            this.WriteEvent(4, message);
        }

        /// <inheritdoc />
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