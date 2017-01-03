namespace WinUX.Design.Material.ColorSwatches
{
    using System;

    using Windows.UI;

    internal sealed class GreyColorSwatch : IMaterialColorSwatch
    {
        public Color Color50 => "#fafafa".ToColor();

        public Color Color100 => "#f5f5f5".ToColor();

        public Color Color200 => "#eeeeee".ToColor();

        public Color Color300 => "#e0e0e0".ToColor();

        public Color Color400 => "#bdbdbd".ToColor();

        public Color Color500 => "#9e9e9e".ToColor();

        public Color Color600 => "#757575".ToColor();

        public Color Color700 => "#616161".ToColor();

        public Color Color800 => "#424242".ToColor();

        public Color Color900 => "#212121".ToColor();

        public Color ColorA100
        {
            get
            {
                throw new NotSupportedException("Grey does not support color A100.");
            }
        }

        public Color ColorA200
        {
            get
            {
                throw new NotSupportedException("Grey does not support color A200.");
            }
        }

        public Color ColorA400
        {
            get
            {
                throw new NotSupportedException("Grey does not support color A400.");
            }
        }

        public Color ColorA700
        {
            get
            {
                throw new NotSupportedException("Grey does not support color A700.");
            }
        }
    }
}
