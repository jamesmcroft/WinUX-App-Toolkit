namespace WinUX
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Defines a collection of extensions for Linq expressions.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the property name from a property expression.
        /// </summary>
        /// <param name="propertyExpression">
        /// The property expression.
        /// </param>
        /// <typeparam name="T">
        /// The type of value.
        /// </typeparam>
        /// <returns>
        /// Returns the string name of the specified property.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the property expression is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the argument is invalid or not a property.
        /// </exception>
        public static string GetPropertyName<T>(this Expression<Func<T>> propertyExpression)
        {
            if (object.Equals(propertyExpression, null))
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            var body = propertyExpression.Body as MemberExpression;

            if (object.Equals(body, null))
            {
                throw new ArgumentException("Invalid argument", nameof(propertyExpression));
            }

            var property = body.Member as PropertyInfo;

            if (object.Equals(property, null))
            {
                throw new ArgumentException("Argument is not a property", nameof(propertyExpression));
            }

            return property.Name;
        }
    }
}