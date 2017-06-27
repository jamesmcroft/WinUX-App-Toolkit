namespace WinUX.Design.Material.ColorSwatches
{
    using System;

    using Android.Graphics;

    /// <summary>
    /// Defines the brown material design color swatch.
    /// </summary>
    internal sealed class BrownColorSwatch : IMaterialColorSwatch<Color>
    {
        /// <inheritdoc />
        public Color Color50 => "#efebe9".ToColor();

        /// <inheritdoc />
        public Color Color100 => "#d7ccc8".ToColor();

        /// <inheritdoc />
        public Color Color200 => "#bcaaa4".ToColor();

        /// <inheritdoc />
        public Color Color300 => "#a1887f".ToColor();

        /// <inheritdoc />
        public Color Color400 => "#8d6e63".ToColor();

        /// <inheritdoc />
        public Color Color500 => "#795548".ToColor();

        /// <inheritdoc />
        public Color Color600 => "#6d4c41".ToColor();

        /// <inheritdoc />
        public Color Color700 => "#5d4037".ToColor();

        /// <inheritdoc />
        public Color Color800 => "#4e342e".ToColor();

        /// <inheritdoc />
        public Color Color900 => "#3e2723".ToColor();

        public Color ColorA100
        {
            get
            {
                throw new NotSupportedException("Brown does not support color A100.");
            }
        }

        public Color ColorA200
        {
            get
            {
                throw new NotSupportedException("Brown does not support color A200.");
            }
        }

        public Color ColorA400
        {
            get
            {
                throw new NotSupportedException("Brown does not support color A400.");
            }
        }

        public Color ColorA700
        {
            get
            {
                throw new NotSupportedException("Brown does not support color A700.");
            }
        }
    }
}