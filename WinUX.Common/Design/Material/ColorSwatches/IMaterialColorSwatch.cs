namespace WinUX.Design.Material.ColorSwatches
{
    /// <summary>
    /// Defines an interface for a material design color swatch.
    /// </summary>
    /// <typeparam name="T">
    /// The type of platform specific Color to map to.
    /// </typeparam>
    /// <remarks>
    /// Defines an interface for a material design color swatch. More info: https://material.google.com/style/color.html
    /// </remarks>
    public interface IMaterialColorSwatch<out T>
    {
        /// <summary>
        /// Gets the 50 color variant.
        /// </summary>
        T Color50 { get; }

        /// <summary>
        /// Gets the 100 (light accent) color variant.
        /// </summary>
        T Color100 { get; }

        /// <summary>
        /// Gets the 200 color variant.
        /// </summary>
        T Color200 { get; }

        /// <summary>
        /// Gets the 300 color variant.
        /// </summary>
        T Color300 { get; }

        /// <summary>
        /// Gets the 400 color variant.
        /// </summary>
        T Color400 { get; }

        /// <summary>
        /// Gets the 500 (primary accent) color variant.
        /// </summary>
        T Color500 { get; }

        /// <summary>
        /// Gets the 600 color variant.
        /// </summary>
        T Color600 { get; }

        /// <summary>
        /// Gets the 700 (dark accent) color variant.
        /// </summary>
        T Color700 { get; }

        /// <summary>
        /// Gets the 800 color variant.
        /// </summary>
        T Color800 { get; }

        /// <summary>
        /// Gets the 900 color variant.
        /// </summary>
        T Color900 { get; }

        /// <summary>
        /// Gets the A100 color variant.
        /// </summary>
        T ColorA100 { get; }

        /// <summary>
        /// Gets the A200 color variant.
        /// </summary>
        T ColorA200 { get; }

        /// <summary>
        /// Gets the A400 color variant.
        /// </summary>
        T ColorA400 { get; }

        /// <summary>
        /// Gets the A700 color variant.
        /// </summary>
        T ColorA700 { get; }
    }
}