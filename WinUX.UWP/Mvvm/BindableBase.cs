namespace WinUX.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    using Windows.ApplicationModel;
    using Windows.UI.Core;
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines a bindable base class.
    /// </summary>
    public abstract class BindableBase : DependencyObject, IBindable
    {
        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (DesignMode.DesignModeEnabled) return;

            var handler = this.PropertyChanged;

            if (object.Equals(handler, null))
            {
                return;
            }

            var args = new PropertyChangedEventArgs(propertyName);
            try
            {
                handler.Invoke(this, args);
            }
            catch
            {
                Window.Current.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () => handler.Invoke(this, args)).AsTask().Wait();
            }
        }

        /// <summary>
        /// Sets the value of a container and updates the UI if set.
        /// </summary>
        /// <param name="valueContainer">
        /// The variable that you are referring to.
        /// </param>
        /// <param name="value">
        /// The new value.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property to refresh the UI with.
        /// </param>
        /// <typeparam name="T">
        /// The type of value.
        /// </typeparam>
        /// <returns>
        /// Returns a boolean value if set occurs.
        /// </returns>
        public virtual bool Set<T>(ref T valueContainer, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(valueContainer, value))
            {
                return false;
            }
            valueContainer = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Sets the value of a container and updates the UI if set.
        /// </summary>
        /// <param name="propertyExpression">
        /// The property expression.
        /// </param>
        /// <param name="valueContainer">
        /// The variable that you are referring to.
        /// </param>
        /// <param name="newValue">
        /// The new value.
        /// </param>
        /// <typeparam name="T">
        /// The type of value.
        /// </typeparam>
        /// <returns>
        /// Returns a boolean value if set occurs.
        /// </returns>
        public virtual bool Set<T>(Expression<Func<T>> propertyExpression, ref T valueContainer, T newValue)
        {
            if (object.Equals(valueContainer, newValue))
            {
                return false;
            }

            valueContainer = newValue;
            this.RaisePropertyChanged(propertyExpression);
            return true;
        }

        /// <summary>
        /// Called to notify that a property has changed.
        /// </summary>
        /// <typeparam name="T">
        /// The type of value.
        /// </typeparam>
        /// <param name="propertyExpression">
        /// The property expression.
        /// </param>
        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (DesignMode.DesignModeEnabled) return;

            var handler = this.PropertyChanged;

            if (!object.Equals(handler, null))
            {
                var propertyName = propertyExpression.GetPropertyName();

                if (!object.Equals(propertyName, null))
                {
                    var args = new PropertyChangedEventArgs(propertyName);
                    try
                    {
                        handler.Invoke(this, args);
                    }
                    catch
                    {
                        Window.Current.CoreWindow.Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () => handler.Invoke(this, args)).AsTask().Wait();
                    }
                }
            }
        }
    }
}