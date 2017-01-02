namespace WinUX.UWP.Extensions
{
    using System;
    using System.Linq.Expressions;

    using Windows.UI.Xaml;

    using WinUX.UWP.Xaml;

    /// <summary>
    /// Defines a collection of extensions for the <see cref="DependencyObject"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Listens to value changes of the specified property on the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of property.
        /// </typeparam>
        /// <param name="obj">
        /// The object to listen to the specified property on.
        /// </param>
        /// <param name="propertyExpression">
        /// The expression containing the property to listen to.
        /// </param>
        /// <param name="handler">
        /// The handler for when the value changes.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public static IDisposable ListenToProperty<T>(
            this DependencyObject obj,
            Expression<Func<T>> propertyExpression,
            DependencyPropertyChangedEventHandler handler)
        {
            var propertyName = propertyExpression.GetPropertyName();

            return ListenToProperty(obj, propertyName, handler);
        }

        /// <summary>
        /// Listens to value changes of the specified property on the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The object to listen to the specified property on.
        /// </param>
        /// <param name="property">
        /// The property to listen to.
        /// </param>
        /// <param name="handler">
        /// The handler for when the value changes.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public static IDisposable ListenToProperty(
            this DependencyObject obj,
            string property,
            DependencyPropertyChangedEventHandler handler)
        {
            return new DependencyPropertyListener(obj, property, handler);
        }
    }
}