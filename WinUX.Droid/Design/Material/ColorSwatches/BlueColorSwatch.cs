namespace WinUX.Design.Material.ColorSwatches
{
    using Android.Graphics;

    /// <summary>
    /// Defines the blue material design color swatch.
    /// </summary>
    internal sealed class BlueColorSwatch : IMaterialColorSwatch<Color>
    {
        /// <inheritdoc />
        public Color Color50 => "#e3f2fd".ToColor();

        /// <inheritdoc />
        public Color Color100 => "#bbdefb".ToColor();

        /// <inheritdoc />
        public Color Color200 => "#90caf9".ToColor();

        /// <inheritdoc />
        public Color Color300 => "#64b5f6".ToColor();

        /// <inheritdoc />
        public Color Color400 => "#42a5f5".ToColor();

        /// <inheritdoc />
        public Color Color500 => "#2196f3".ToColor();

        /// <inheritdoc />
        public Color Color600 => "#1e88e5".ToColor();

        /// <inheritdoc />
        public Color Color700 => "#1976d2".ToColor();

        /// <inheritdoc />
        public Color Color800 => "#1565c0".ToColor();

        /// <inheritdoc />
        public Color Color900 => "#0d47a1".ToColor();

        /// <inheritdoc />
        public Color ColorA100 => "#82b1ff".ToColor();

        /// <inheritdoc />
        public Color ColorA200 => "#448aff".ToColor();

        /// <inheritdoc />
        public Color ColorA400 => "#2979ff".ToColor();

        /// <inheritdoc />
        public Color ColorA700 => "#2962ff".ToColor();
    }
}