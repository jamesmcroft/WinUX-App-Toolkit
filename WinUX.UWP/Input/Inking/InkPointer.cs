namespace WinUX.Input.Inking
{
    using System;

    using Windows.UI.Core;

    /// <summary>
    /// Handler for when an inking pointer interacts with a drawing canvas.
    /// </summary>
    /// <param name="sender">
    /// The originator.
    /// </param>
    /// <param name="args">
    /// The ink pointer arguments containing the properties of the input device.
    /// </param>
    public delegate void InkPointerEventHandler(object sender, InkPointerEventArgs args);

    /// <summary>
    /// Defines event arguments for when an ink pointer event is fired.
    /// </summary>
    public class InkPointerEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InkPointerEventArgs"/> class.
        /// </summary>
        /// <param name="pointerId">
        /// The pointer identifier.
        /// </param>
        /// <param name="originatingArgs">
        /// The originating <see cref="PointerEventArgs"/>.
        /// </param>
        public InkPointerEventArgs(int pointerId, PointerEventArgs originatingArgs)
        {
            this.PointerId = pointerId;
            this.Properties = originatingArgs;
        }

        /// <summary>
        /// Gets the pointer identifier.
        /// </summary>
        public int PointerId { get; }

        /// <summary>
        /// Gets the pointer properties.
        /// </summary>
        public PointerEventArgs Properties { get; }
    }
}