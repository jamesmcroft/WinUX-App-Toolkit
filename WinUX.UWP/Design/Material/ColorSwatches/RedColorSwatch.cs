namespace WinUX.Design.Material.ColorSwatches
{
    using Windows.UI;

    using WinUX.Extensions;

    internal sealed class RedColorSwatch : IMaterialColorSwatch
    {
        public Color Color50 => "#ffebee".ToColor();

        public Color Color100 => "#ffcdd2".ToColor();

        public Color Color200 => "#ef9a9a".ToColor();

        public Color Color300 => "#e57373".ToColor();

        public Color Color400 => "#ef5350".ToColor();

        public Color Color500 => "#f44336".ToColor();

        public Color Color600 => "#e53935".ToColor();

        public Color Color700 => "#d32f2f".ToColor();

        public Color Color800 => "#c62828".ToColor();

        public Color Color900 => "#b71c1c".ToColor();

        public Color ColorA100 => "#ff8a80".ToColor();

        public Color ColorA200 => "#ff5252".ToColor();

        public Color ColorA400 => "#ff1744".ToColor();

        public Color ColorA700 => "#d50000".ToColor();
    }
}