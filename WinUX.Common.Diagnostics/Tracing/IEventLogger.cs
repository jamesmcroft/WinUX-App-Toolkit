namespace WinUX.Diagnostics.Tracing
{
    public interface IEventLogger
    {
        /// <summary>
        /// Writes a debug information message to the event log.
        /// </summary>
        /// <param name="message">
        /// The debug information message to write out.
        /// </param>
        /// <remarks>
        /// This will only write out if the application is running in debug mode.
        /// </remarks>
        void WriteDebug(string message);

        /// <summary>
        /// Writes a generic information message to the event log.
        /// </summary>
        /// <param name="message">
        /// The generic information message to write out.
        /// </param>
        void WriteInfo(string message);

        /// <summary>
        /// Writes a warning message to the event log.
        /// </summary>
        /// <param name="message">
        /// The warning message to write out.
        /// </param> 
        void WriteWarning(string message);

        /// <summary>
        /// Writes an error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The error message to write out.
        /// </param>
        void WriteError(string message);

        /// <summary>
        /// Writes a critical message to the event log.
        /// </summary>
        /// <param name="message">
        /// The critical message to write out.
        /// </param>
        void WriteCritical(string message);
    }
}