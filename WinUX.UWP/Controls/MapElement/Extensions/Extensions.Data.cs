namespace WinUX.Controls.MapElement
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls.Maps;

    /// <summary>
    /// Defines an extension to the <see cref="MapElement"/> control for binding an associated data context.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Defines the dependency property for the data.
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.RegisterAttached(
            "Data",
            typeof(object),
            typeof(Extensions),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets the data object for the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/>.
        /// </param>
        /// <returns>
        /// Returns the data as an object.
        /// </returns>
        public static object GetData(DependencyObject obj)
        {
            return obj.GetValue(DataProperty);
        }

        /// <summary>
        /// Sets the data object of the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> to set the data to.
        /// </param>
        /// <param name="value">
        /// The data to set.
        /// </param>
        public static void SetData(DependencyObject obj, object value)
        {
            obj.SetValue(DataProperty, value);
        }

        /// <summary>
        /// Sets the data of the specified <see cref="MapElement"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="MapElement"/> to set the data to.
        /// </param>
        /// <param name="data">
        /// The data to set.
        /// </param>
        public static void SetData(this MapElement element, object data)
        {
            SetData((DependencyObject)element, data);
        }

        /// <summary>
        /// Gets the data from the specified map element.
        /// </summary>
        /// <param name="element">
        /// The <see cref="MapElement"/> to get the data from.
        /// </param>
        /// <typeparam name="T">
        /// The expected data type.
        /// </typeparam>
        /// <returns>
        /// Returns the data as the expected type.
        /// </returns>
        public static T GetData<T>(this MapElement element) where T : class
        {
            return GetData(element) as T;
        }
    }
}