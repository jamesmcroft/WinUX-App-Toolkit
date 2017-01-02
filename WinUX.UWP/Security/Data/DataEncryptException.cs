namespace WinUX.UWP.Security.Data
{
    using System;

    /// <summary>
    /// Defines an exception for when an exception is thrown during data encryption or decryption.
    /// </summary>
    public class DataEncryptException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEncryptException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public DataEncryptException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataEncryptException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public DataEncryptException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}