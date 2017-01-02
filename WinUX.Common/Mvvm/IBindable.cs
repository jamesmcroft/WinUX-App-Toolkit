namespace WinUX.Mvvm
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines an interface for a bindable object.
    /// </summary>
    public interface IBindable : INotifyPropertyChanged
    {
        /// <summary>
        /// Called to notify that a property has changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        void RaisePropertyChanged([CallerMemberName] string propertyName = null);
    }
}