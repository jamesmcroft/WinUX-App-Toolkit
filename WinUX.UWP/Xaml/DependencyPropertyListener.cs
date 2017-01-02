namespace WinUX.UWP.Xaml
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a helper for listening to changes of a property of a DependencyObject.
    /// </summary>
    public class DependencyPropertyListener : DependencyObject, IDisposable
    {
        /// <summary>
        /// Defines the dependency property for the Value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(object),
            typeof(DependencyPropertyListener),
            new PropertyMetadata(null, (d, e) => ((DependencyPropertyListener)d).OnValueChanged(e)));

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyPropertyListener"/> class.
        /// </summary>
        /// <param name="obj">
        /// The dependency object.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="eventHandler">
        /// The event handler.
        /// </param>
        public DependencyPropertyListener(
            DependencyObject obj,
            string propertyName,
            DependencyPropertyChangedEventHandler eventHandler)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (eventHandler == null) throw new ArgumentNullException(nameof(eventHandler));

            this.eventHandler = eventHandler;

            var binding = new Binding { Source = obj, Path = new PropertyPath(propertyName), Mode = BindingMode.OneWay };

            BindingOperations.SetBinding(this, ValueProperty, binding);
        }

        private DependencyPropertyChangedEventHandler eventHandler;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.eventHandler = null;
                BindingOperations.SetBinding(this, ValueProperty, new Binding());
            }
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            this.eventHandler?.Invoke(this, e);
        }
    }
}