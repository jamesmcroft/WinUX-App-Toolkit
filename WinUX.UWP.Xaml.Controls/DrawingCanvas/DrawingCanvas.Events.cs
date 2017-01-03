namespace WinUX.Xaml.Controls
{
    using WinUX.Input.Inking;

    /// <summary>
    /// Defines the events for the <see cref="DrawingCanvas"/>.
    /// </summary>
    public sealed partial class DrawingCanvas
    {
        /// <summary>
        /// The ink rendered event.
        /// </summary>
        public event InkRenderedEventHandler InkRendered;

        /// <summary>
        /// The ink changed event.
        /// </summary>
        public event InkChangedEventHandler InkChanged;

        /// <summary>
        /// The ink pointer entering event.
        /// </summary>
        public event InkPointerEventHandler InkPointerEntering;

        /// <summary>
        /// The ink pointer exiting event.
        /// </summary>
        public event InkPointerEventHandler InkPointerExiting;

        /// <summary>
        /// The ink pointer hovering event.
        /// </summary>
        public event InkPointerEventHandler InkPointerHovering;

        /// <summary>
        /// The ink pointer lost event.
        /// </summary>
        public event InkPointerEventHandler InkPointerLost;

        /// <summary>
        /// The ink pointer moving event.
        /// </summary>
        public event InkPointerEventHandler InkPointerMoving;

        /// <summary>
        /// The ink pointer pressing event.
        /// </summary>
        public event InkPointerEventHandler InkPointerPressing;

        /// <summary>
        /// The ink pointer releasing event.
        /// </summary>
        public event InkPointerEventHandler InkPointerReleasing;
    }
}