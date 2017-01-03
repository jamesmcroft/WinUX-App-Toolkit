namespace WinUX.Input.Inking
{
    using System;
    using System.Collections.Generic;

    using Windows.UI.Input.Inking;

    /// <summary>
    /// Handler for when ink has changed.
    /// </summary>
    /// <param name="sender">
    /// The originator.
    /// </param>
    /// <param name="args">
    /// The ink changed arguments containing the changed ink strokes.
    /// </param>
    public delegate void InkChangedEventHandler(object sender, InkChangedEventArgs args);

    /// <summary>
    /// Defines event arguments for when ink strokes have changed.
    /// </summary>
    public sealed class InkChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InkChangedEventArgs"/> class.
        /// </summary>
        public InkChangedEventArgs()
            : this(null, null, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InkChangedEventArgs"/> class.
        /// </summary>
        /// <param name="addedStrokes">
        /// The added strokes.
        /// </param>
        /// <param name="removedStrokes">
        /// The removed strokes.
        /// </param>
        /// <param name="cleared">
        /// An indicator to whether the ink was a result of a clear.
        /// </param>
        public InkChangedEventArgs(
            IEnumerable<InkStroke> addedStrokes,
            IEnumerable<InkStroke> removedStrokes,
            bool cleared)
        {
            this.AddedStrokes = addedStrokes;
            this.RemovedStrokes = removedStrokes;
            this.Cleared = cleared;
        }

        /// <summary>
        /// Gets the associated added strokes.
        /// </summary>
        public IEnumerable<InkStroke> AddedStrokes { get; private set; }

        /// <summary>
        /// Gets the associated removed strokes.
        /// </summary>
        public IEnumerable<InkStroke> RemovedStrokes { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the canvas was cleared.
        /// </summary>
        public bool Cleared { get; private set; }
    }
}