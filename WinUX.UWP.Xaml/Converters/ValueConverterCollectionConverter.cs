namespace WinUX.Xaml.Converters
{
    using System;
    using System.Linq;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    using WinUX.Xaml.Converters.Collections;

    /// <summary>
    /// Defines a value converter for converting a value through multiple value converters to get a desired result.
    /// </summary>
    public sealed class ValueConverterCollectionConverter : DependencyObject, IValueConverter
    {
        /// <summary>
        /// Defines the dependency property for <see cref="ConverterCollection"/>.
        /// </summary>
        public static readonly DependencyProperty ConverterCollectionProperty =
            DependencyProperty.Register(
                nameof(ConverterCollection),
                typeof(ValueConverterCollection),
                typeof(ValueConverterCollectionConverter),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the collection of value converters.
        /// </summary>
        public ValueConverterCollection ConverterCollection
        {
            get
            {
                return (ValueConverterCollection)this.GetValue(ConverterCollectionProperty);
            }
            set
            {
                this.SetValue(ConverterCollectionProperty, value);
            }
        }

        /// <summary>
        /// Converts the specified <see cref="value"/> through the <see cref="ConverterCollection"/>.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// Returns a converted result.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (this.ConverterCollection != null)
            {
                value =
                    this.ConverterCollection.Converters.Where(converter => converter.Converter != null)
                        .Aggregate(value, (current, converter) => converter.Convert(current, targetType));
            }

            return value;
        }

        /// <summary>
        /// Converts a converted result back to the original value through the <see cref="ConverterCollection"/>.
        /// </summary>
        /// <remarks>
        /// If a ConvertBack method has not been implemented, the expected convert back result may be incorrect.
        /// </remarks>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// Returns the original value.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (this.ConverterCollection != null)
            {
                value =
                    this.ConverterCollection.Converters.Where(converter => converter.Converter != null)
                        .Reverse()
                        .Aggregate(value, (current, converter) => converter.ConvertBack(current, targetType));
            }

            return value;
        }
    }
}