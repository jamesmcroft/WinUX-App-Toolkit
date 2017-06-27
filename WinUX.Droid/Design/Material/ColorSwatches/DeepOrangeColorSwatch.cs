namespace WinUX.Design.Material.ColorSwatches
{
    using Android.Graphics;

    /// <summary>
    /// Defines the deep orange material design color swatch.
    /// </summary>
    internal sealed class DeepOrangeColorSwatch : IMaterialColorSwatch<Color>
    {
        /// <inheritdoc />
        public Color Color50 => "#fbe9e7".ToColor();

        /// <inheritdoc />
        public Color Color100 => "#ffccbc".ToColor();

        /// <inheritdoc />
        public Color Color200 => "#ffab91".ToColor();

        /// <inheritdoc />
        public Color Color300 => "#ff8a65".ToColor();

        /// <inheritdoc />
        public Color Color400 => "#ff7043".ToColor();

        /// <inheritdoc />
        public Color Color500 => "#ff5722".ToColor();

        /// <inheritdoc />
        public Color Color600 => "#f4511e".ToColor();

        /// <inheritdoc />
        public Color Color700 => "#e64a19".ToColor();

        /// <inheritdoc />
        public Color Color800 => "#d84315".ToColor();

        /// <inheritdoc />
        public Color Color900 => "#bf360c".ToColor();

        /// <inheritdoc />
        public Color ColorA100 => "#ff9e80".ToColor();

        /// <inheritdoc />
        public Color ColorA200 => "#ff6e40".ToColor();

        /// <inheritdoc />
        public Color ColorA400 => "#ff3d00".ToColor();

        /// <inheritdoc />
        public Color ColorA700 => "#dd2c00".ToColor();
    }
}