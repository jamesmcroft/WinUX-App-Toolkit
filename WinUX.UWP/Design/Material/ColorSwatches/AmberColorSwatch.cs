namespace WinUX.Design.Material.ColorSwatches
{
    using Windows.UI;

    using WinUX.Extensions;

    /// <summary>
    /// Defines the amber material design color swatch.
    /// </summary>
    internal sealed class AmberColorSwatch : IMaterialColorSwatch
    {
        /// <inheritdoc />
        public Color Color50 => "#fff8e1".ToColor();

        /// <inheritdoc />
        public Color Color100 => "#ffecb3".ToColor();

        /// <inheritdoc />
        public Color Color200 => "#ffe082".ToColor();

        /// <inheritdoc />
        public Color Color300 => "#ffd54f".ToColor();

        /// <inheritdoc />
        public Color Color400 => "#ffca28".ToColor();

        /// <inheritdoc />
        public Color Color500 => "#ffc107".ToColor();

        /// <inheritdoc />
        public Color Color600 => "#ffb300".ToColor();

        /// <inheritdoc />
        public Color Color700 => "#ffa000".ToColor();

        /// <inheritdoc />
        public Color Color800 => "#ff8f00".ToColor();

        /// <inheritdoc />
        public Color Color900 => "#ff6f00".ToColor();

        /// <inheritdoc />
        public Color ColorA100 => "#ffe57f".ToColor();

        /// <inheritdoc />
        public Color ColorA200 => "#ffd740".ToColor();

        /// <inheritdoc />
        public Color ColorA400 => "#ffc400".ToColor();

        /// <inheritdoc />
        public Color ColorA700 => "#ffab00".ToColor();
    }
}