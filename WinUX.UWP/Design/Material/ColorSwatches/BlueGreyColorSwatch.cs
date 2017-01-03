namespace WinUX.Design.Material.ColorSwatches
{
    using System;

    using Windows.UI;

    using WinUX.Extensions;

    /// <summary>
    /// Defines the blue-grey material design color swatch.
    /// </summary>
    internal sealed class BlueGreyColorSwatch : IMaterialColorSwatch
    {
        /// <inheritdoc />
        public Color Color50 => "#eceff1".ToColor();

        /// <inheritdoc />
        public Color Color100 => "#cfd8dc".ToColor();

        /// <inheritdoc />
        public Color Color200 => "#b0bec5".ToColor();

        /// <inheritdoc />
        public Color Color300 => "#90a4ae".ToColor();

        /// <inheritdoc />
        public Color Color400 => "#78909c".ToColor();

        /// <inheritdoc />
        public Color Color500 => "#607d8b".ToColor();

        /// <inheritdoc />
        public Color Color600 => "#546e7a".ToColor();

        /// <inheritdoc />
        public Color Color700 => "#455a64".ToColor();

        /// <inheritdoc />
        public Color Color800 => "#37474f".ToColor();

        /// <inheritdoc />
        public Color Color900 => "#263238".ToColor();

        /// <summary>
        /// Color A100 is not supported by the color swatch.
        /// </summary>
        public Color ColorA100
        {
            get
            {
                throw new NotSupportedException("Blue grey does not support color A100.");
            }
        }

        /// <summary>
        /// Color A200 is not supported by the color swatch.
        /// </summary>
        public Color ColorA200
        {
            get
            {
                throw new NotSupportedException("Blue grey does not support color A200.");
            }
        }

        /// <summary>
        /// Color A400 is not supported by the color swatch.
        /// </summary>
        public Color ColorA400
        {
            get
            {
                throw new NotSupportedException("Blue grey does not support color A400.");
            }
        }

        /// <summary>
        /// Color A700 is not supported by the color swatch.
        /// </summary>
        public Color ColorA700
        {
            get
            {
                throw new NotSupportedException("Blue grey does not support color A700.");
            }
        }
    }
}