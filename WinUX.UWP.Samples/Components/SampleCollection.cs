namespace WinUX.UWP.Samples.Components
{
    using Windows.UI.Xaml.Controls;

    public sealed class SampleCollection
    {
        /// <summary>
        /// Gets or sets the collection's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the source page type for the sample collection.
        /// </summary>
        public string SourcePageType { get; set; }

        /// <summary>
        /// Gets or sets the icon path data.
        /// </summary>
        public string IconPathData { get; set; }

        /// <summary>
        /// Gets or sets the icon symbol.
        /// </summary>
        public Symbol IconSymbol { get; set; }

        /// <summary>
        /// Gets or sets the samples in the collection.
        /// </summary>
        public Sample[] Samples { get; set; }
    }
}