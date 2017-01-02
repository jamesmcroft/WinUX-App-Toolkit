namespace WinUX.Data
{
    using System;

    /// <summary>
    /// Handler for when a value changes.
    /// </summary>
    /// <param name="sender">
    /// The originater.
    /// </param>
    /// <param name="e">
    /// The value changed arguments containing the changes values..
    /// </param>
    /// <typeparam name="T">
    /// The type of value.
    /// </typeparam>
    public delegate void ValueChangedEventHandler<T>(object sender, ValueChangedEventArgs<T> e);

    /// <summary>
    /// Defines the event arguments for when a value changes.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of value.
    /// </typeparam>
    public class ValueChangedEventArgs<TValue> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueChangedEventArgs{TValue}"/> class.
        /// </summary>
        /// <param name="oldValue">
        /// The old value.
        /// </param>
        /// <param name="newValue">
        /// The new value.
        /// </param>
        public ValueChangedEventArgs(TValue oldValue, TValue newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        /// <summary>
        /// Gets the old value.
        /// </summary>
        public TValue OldValue { get; }

        /// <summary>
        /// Gets the new value.
        /// </summary>
        public TValue NewValue { get; }
    }
}