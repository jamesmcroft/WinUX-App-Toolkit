namespace WinUX.UWP.Design.Material.ColorSwatches
{
    using Windows.UI;

    /// <summary>
    /// Defines an interface for a material design color swatch.
    /// </summary>
    /// <remarks>
    /// Defines an interface for a material design color swatch. More info: https://material.google.com/style/color.html
    /// </remarks>
    public interface IMaterialColorSwatch
    {
        /// <summary>
        /// Gets the 50 color variant.
        /// </summary>
        Color Color50 { get; }

        /// <summary>
        /// Gets the 100 (light accent) color variant.
        /// </summary>
        Color Color100 { get; }

        /// <summary>
        /// Gets the 200 color variant.
        /// </summary>
        Color Color200 { get; }

        /// <summary>
        /// Gets the 300 color variant.
        /// </summary>
        Color Color300 { get; }

        /// <summary>
        /// Gets the 400 color variant.
        /// </summary>
        Color Color400 { get; }

        /// <summary>
        /// Gets the 500 (primary accent) color variant.
        /// </summary>
        Color Color500 { get; }

        /// <summary>
        /// Gets the 600 color variant.
        /// </summary>
        Color Color600 { get; }

        /// <summary>
        /// Gets the 700 (dark accent) color variant.
        /// </summary>
        Color Color700 { get; }

        /// <summary>
        /// Gets the 800 color variant.
        /// </summary>
        Color Color800 { get; }

        /// <summary>
        /// Gets the 900 color variant.
        /// </summary>
        Color Color900 { get; }

        /// <summary>
        /// Gets the A100 color variant.
        /// </summary>
        Color ColorA100 { get; }

        /// <summary>
        /// Gets the A200 color variant.
        /// </summary>
        Color ColorA200 { get; }

        /// <summary>
        /// Gets the A400 color variant.
        /// </summary>
        Color ColorA400 { get; }

        /// <summary>
        /// Gets the A700 color variant.
        /// </summary>
        Color ColorA700 { get; }
    }
}