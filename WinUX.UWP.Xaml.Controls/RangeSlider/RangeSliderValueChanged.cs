namespace WinUX.Xaml.Controls
{
    using WinUX.Data;

    /// <summary>
    /// Handler for when a thumb value has changed.
    /// </summary>
    /// <param name="sender">
    /// The originator.
    /// </param>
    /// <param name="args">
    /// The thumb value changed arguments containing the old and new values.
    /// </param>
    public delegate void RangeSliderValueChangedEventHandler(object sender, RangeSliderValueChangedEventArgs args);

    /// <summary>
    /// Defines the event arguments for when a value changes on the <see cref="RangeSlider"/> control.
    /// </summary>
    public class RangeSliderValueChangedEventArgs : ValueChangedEventArgs<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeSliderValueChangedEventArgs"/> class.
        /// </summary>
        /// <param name="thumb">
        /// The changed thumb.
        /// </param>
        /// <param name="oldValue">
        /// The old value.
        /// </param>
        /// <param name="newValue">
        /// The new value.
        /// </param>
        public RangeSliderValueChangedEventArgs(RangeSliderThumb thumb, double oldValue, double newValue)
            : base(oldValue, newValue)
        {
            this.Thumb = thumb;
        }

        /// <summary>
        /// Gets the changed thumb.
        /// </summary>
        public RangeSliderThumb Thumb { get; private set; }
    }
}