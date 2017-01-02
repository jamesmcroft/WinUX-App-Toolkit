namespace WinUX.Exceptions
{
    using System;

    /// <summary>
    /// Defines an exception that is thrown when a property is null when not expected.
    /// </summary>
    public class UnexpectedNullPropertyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedNullPropertyException"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property.
        /// </param>
        /// <param name="expectedType">
        /// The expected type for the property.
        /// </param>
        public UnexpectedNullPropertyException(string propertyName, Type expectedType)
            : this(propertyName, expectedType, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedNullPropertyException"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property.
        /// </param>
        /// <param name="expectedType">
        /// The expected type for the property.
        /// </param>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        public UnexpectedNullPropertyException(string propertyName, Type expectedType, string message)
            : this(propertyName, expectedType, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedNullPropertyException"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property.
        /// </param>
        /// <param name="expectedType">
        /// The expected type for the property.
        /// </param>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception.
        /// </param>
        public UnexpectedNullPropertyException(
            string propertyName,
            Type expectedType,
            string message,
            Exception innerException)
            : base(message, innerException)
        {
            this.PropertyName = propertyName;
            this.ExpectedType = expectedType;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Gets the expected type for the property.
        /// </summary>
        public Type ExpectedType { get; }
    }
}