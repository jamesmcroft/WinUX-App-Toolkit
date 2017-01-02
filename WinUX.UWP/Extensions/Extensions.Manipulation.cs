namespace WinUX.UWP.Extensions
{
    using Windows.UI.Xaml.Input;

    using WinUX.UWP.Input.Manipulation;

    /// <summary>
    /// Defines a collection of extensions for handling manipulation.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the swipe direction of a manipulation.
        /// </summary>
        /// <param name="e">
        /// The manipulation delta event arguments.
        /// </param>
        /// <param name="threshold">
        /// The threshold to check.
        /// </param>
        /// <returns>
        /// The <see cref="SwipeDirection"/>.
        /// </returns>
        public static SwipeDirection GetSwipeDirection(
            this ManipulationDeltaRoutedEventArgs e,
            double threshold)
        {
            return e.Cumulative.Translation.X >= threshold
                       ? SwipeDirection.Right
                       : (e.Cumulative.Translation.X <= -threshold
                              ? SwipeDirection.Left
                              : (e.Cumulative.Translation.Y >= threshold
                                     ? SwipeDirection.Up
                                     : (e.Cumulative.Translation.Y <= -threshold
                                            ? SwipeDirection.Down
                                            : SwipeDirection.None)));
        }

        /// <summary>
        /// Gets the swipe direction of a manipulation.
        /// </summary>
        /// <param name="cumulativeValue">
        /// The cumulative Value.
        /// </param>
        /// <param name="axis">
        /// The axis.
        /// </param>
        /// <param name="threshold">
        /// The threshold to check.
        /// </param>
        /// <returns>
        /// The <see cref="SwipeDirection"/>.
        /// </returns>
        public static SwipeDirection GetSwipeDirection(this double cumulativeValue, SwipeAxis axis, double threshold)
        {
            switch (axis)
            {
                case SwipeAxis.X:
                    if (cumulativeValue >= threshold)
                    {
                        return SwipeDirection.Right;
                    }

                    if (cumulativeValue <= -threshold)
                    {
                        return SwipeDirection.Left;
                    }
                    break;
                case SwipeAxis.Y:
                    if (cumulativeValue >= threshold)
                    {
                        return SwipeDirection.Up;
                    }

                    if (cumulativeValue <= -threshold)
                    {
                        return SwipeDirection.Down;
                    }
                    break;
            }

            return SwipeDirection.None;
        }
    }
}