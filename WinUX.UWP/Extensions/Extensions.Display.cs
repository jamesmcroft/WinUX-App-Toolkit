namespace WinUX
{
    using Windows.Graphics.Display;

    /// <summary>
    /// Defines a collection of extensions for code regarding the device display.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Converts a <see cref="DisplayOrientations"/> value to degrees.
        /// </summary>
        /// <param name="orientation">
        /// The orientation to convert.
        /// </param>
        /// <returns>
        /// Returns the degrees as an <see cref="int"/>.
        /// </returns>
        public static int ToDegrees(this DisplayOrientations orientation)
        {
            switch (orientation)
            {
                case DisplayOrientations.Portrait:
                    return 90;
                case DisplayOrientations.LandscapeFlipped:
                    return 180;
                case DisplayOrientations.PortraitFlipped:
                    return 270;
                default:
                    return 0;
            }
        }
    }
}