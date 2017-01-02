namespace WinUX.UWP.Extensions
{
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Shapes;

    /// <summary>
    /// Defines a collection of extensions for XAML.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Converts a path data string to a <see cref="Path"/>.
        /// </summary>
        /// <param name="pathData">
        /// The path data string.
        /// </param>
        /// <returns>
        /// Returns a <see cref="Path"/>.
        /// </returns>
        public static Path ToPath(this string pathData)
        {
            var xaml = "<Path " + "xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>" + "<Path.Data>"
                       + pathData + "</Path.Data></Path>";
            return XamlReader.Load(xaml) as Path;
        }

        /// <summary>
        /// Converts a path data string to a <see cref="Geometry"/>.
        /// </summary>
        /// <param name="pathData">
        /// The path data string.
        /// </param>
        /// <returns>
        /// Returns a <see cref="Geometry"/>.
        /// </returns>
        public static Geometry ToGeometry(this string pathData)
        {
            var path = pathData.ToPath();
            return path.Data;
        }

        /// <summary>
        /// Converts a path data string to a <see cref="PathIcon"/>.
        /// </summary>
        /// <param name="pathData">
        /// The path data string.
        /// </param>
        /// <returns>
        /// Returns a <see cref="PathIcon"/>.
        /// </returns>
        public static PathIcon ToPathIcon(this string pathData)
        {
            var geometry = pathData.ToGeometry();
            return new PathIcon { Data = geometry };
        }
    }
}