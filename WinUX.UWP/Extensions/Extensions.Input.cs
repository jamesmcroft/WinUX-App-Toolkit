namespace WinUX
{
    using Windows.Devices.Input;
    using Windows.UI.Core;
    using Windows.UI.Input;

    using WinUX.Input.Pointer;

    /// <summary>
    /// Defines a collection of extensions for handling Windows input devices.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Detects whether the given <see cref="PointerPoint"/> is a Surface Hub pen.
        /// </summary>
        /// <param name="pointerPoint">
        /// The pointer point to check.
        /// </param>
        /// <returns>
        /// Returns true if is a Surface Hub pen; else false.
        /// </returns>
        public static bool IsSurfaceHubPen(this PointerPoint pointerPoint)
        {
            return pointerPoint.Properties.HasUsage(0x0D, 0x5b);
        }

        /// <summary>
        /// Gets the pointer identifier for the given <see cref="PointerPoint"/>.
        /// </summary>
        /// <remarks>
        /// This method will attempt to retrieve a unique hardware identifier for the <see cref="PointerPoint"/>.
        /// </remarks>
        /// <param name="pointerPoint">
        /// The pointer point to check.
        /// </param>
        /// <returns>
        /// Returns a <see cref="int"/> identifier.
        /// </returns>
        public static int GetPointerId(this PointerPoint pointerPoint)
        {
            if (pointerPoint.IsSurfaceHubPen())
            {
                return pointerPoint.Properties.GetUsageValue(0x0D, 0x5b);
            }

            // ToDo: Add checks for other hardware inputs (i.e. Surface pen, another inking pen, mouse)

            return (int)pointerPoint.PointerId;
        }

        /// <summary>
        /// Gets the pen's event type for the specified pointer event args.
        /// </summary>
        /// <param name="e">
        /// The pointer event args.
        /// </param>
        /// <returns>
        /// Returns the event type associated with the pointer event.
        /// </returns>
        public static PenEventType GetPenEventType(this PointerEventArgs e)
        {
            var type = PenEventType.None;

            if (e.IsPenEraseMode())
            {
                type = PenEventType.Erase;
            }
            else if (e.IsPenInkMode())
            {
                type = PenEventType.Ink;
            }
            else if (e.IsPenSelectMode())
            {
                type = PenEventType.Select;
            }

            return type;
        }

        /// <summary>
        /// Checks whether the pointer event is an erase event.
        /// </summary>
        /// <param name="e">
        /// The pointer event args.
        /// </param>
        /// <returns>
        /// Returns true if is erase; else false.
        /// </returns>
        public static bool IsPenEraseMode(this PointerEventArgs e)
        {
            var pointerProperties = e.CurrentPoint.Properties;

            var isErasePointer = pointerProperties.IsEraser
                                 || (e.CurrentPoint.PointerDevice.PointerDeviceType == PointerDeviceType.Pen
                                     && !pointerProperties.IsBarrelButtonPressed
                                     && pointerProperties.IsRightButtonPressed);

            return isErasePointer;
        }

        /// <summary>
        /// Checks whether the pointer event is an inking event.
        /// </summary>
        /// <param name="e">
        /// The pointer event args.
        /// </param>
        /// <returns>
        /// Returns true if is inking; else false.
        /// </returns>
        public static bool IsPenInkMode(this PointerEventArgs e)
        {
            var pointerProperties = e.CurrentPoint.Properties;

            var isInkPointer = e.CurrentPoint.PointerDevice.PointerDeviceType == PointerDeviceType.Pen
                               && !pointerProperties.IsBarrelButtonPressed && !pointerProperties.IsRightButtonPressed;


            return isInkPointer;
        }

        /// <summary>
        /// Checks whether the pointer event is a select event.
        /// </summary>
        /// <param name="e">
        /// The pointer event args.
        /// </param>
        /// <returns>
        /// Returns true if is select; else false.
        /// </returns>
        public static bool IsPenSelectMode(this PointerEventArgs e)
        {
            var pointerProperties = e.CurrentPoint.Properties;

            var isSelectPointer = e.CurrentPoint.PointerDevice.PointerDeviceType == PointerDeviceType.Pen
                                  && pointerProperties.IsBarrelButtonPressed;

            return isSelectPointer;
        }
    }
}