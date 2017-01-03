namespace WinUX.Xaml.Converters.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a collection of <see cref="ValueConverter"/> objects.
    /// </summary>
    public sealed class ValueConverterCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverterCollection"/> class.
        /// </summary>
        public ValueConverterCollection()
        {
            this.Converters = new List<ValueConverter>();
        }

        /// <summary>
        /// Gets or sets the converters for the collection.
        /// </summary>
        public List<ValueConverter> Converters { get; set; }
    }
}