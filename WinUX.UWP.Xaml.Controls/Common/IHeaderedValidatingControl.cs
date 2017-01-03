namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines an interface for a headered validating control.
    /// </summary>
    public interface IHeaderedValidatingControl : IValidatingControl
    {
        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        object Header { get; set; }

        /// <summary>
        /// Gets or sets the template for the header.
        /// </summary>
        DataTemplate HeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the visibility of the header.
        /// </summary>
        Visibility HeaderVisibility { get; set; }
    }
}