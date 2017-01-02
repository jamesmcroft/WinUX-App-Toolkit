namespace WinUX.UWP.Services.Jobs
{
    using System;

    /// <summary>
    /// Defines an exception for when the reference to a <see cref="Job"/> is lost.
    /// </summary>
    public class JobReferenceLostException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobReferenceLostException"/> class.
        /// </summary>
        public JobReferenceLostException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobReferenceLostException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        public JobReferenceLostException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobReferenceLostException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public JobReferenceLostException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}