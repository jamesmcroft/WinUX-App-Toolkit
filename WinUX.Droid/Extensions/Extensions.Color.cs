namespace WinUX
{
    using System.Globalization;

    using Android.Graphics;
    using Android.Provider;

    using WinUX.Design;
    using WinUX.Design.Material;

    /// <summary>
    /// Defines a collection of extensions for handling color.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets the Hex representation of the specified <see cref="Android.Graphics.Color"/>.
        /// </summary>
        /// <param name="color">
        /// The <see cref="Android.Graphics.Color"/> to get the Hex value of.
        /// </param>
        /// <returns>
        /// Returns the Hex value as a <see cref="string"/>.
        /// </returns>
        public static string ToHexString(this Color color)
        {
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        /// <summary>
        /// Converts a ARGB or RGB hex value to a <see cref="Android.Graphics.Color"/>.
        /// </summary>
        /// <param name="hexValue">
        /// The ARGB or RGB hex value represented as a string.
        /// </param>
        /// <returns>
        /// Returns the Color representation of the ARGB hex string value.
        /// </returns>
        public static Color ToColor(this string hexValue)
        {
            var val = hexValue.ToUpper();

            Color color = new Color(0, 0, 0, 255);

            switch (val.Length)
            {
                case 7:
                    color = new Color
                    {
                        A = 255,
                        R = byte.Parse(val.Substring(1, 2), NumberStyles.AllowHexSpecifier),
                        G = byte.Parse(val.Substring(3, 2), NumberStyles.AllowHexSpecifier),
                        B = byte.Parse(val.Substring(5, 2), NumberStyles.AllowHexSpecifier)
                    };
                    break;
                case 9:
                    color = new Color
                    {
                        A = byte.Parse(val.Substring(1, 2), NumberStyles.AllowHexSpecifier),
                        R = byte.Parse(val.Substring(3, 2), NumberStyles.AllowHexSpecifier),
                        G = byte.Parse(val.Substring(5, 2), NumberStyles.AllowHexSpecifier),
                        B = byte.Parse(val.Substring(7, 2), NumberStyles.AllowHexSpecifier)
                    };
                    break;
            }

            return color;
        }

        /// <summary>
        /// Lightens a color by a given amount.
        /// </summary>
        /// <param name="color">
        /// The <see cref="Android.Graphics.Color"/> to lighten.
        /// </param>
        /// <param name="amount">
        /// The amount to lighten by.
        /// </param>
        /// <returns>
        /// Returns the lightened color as a <see cref="Android.Graphics.Color"/>.
        /// </returns>
        public static Color Lighten(this Color color, float amount)
        {
            var val = amount * .01;
            return Lerp(color, (float)val);
        }

        /// <summary>
        /// Darkens a color by a given amount.
        /// </summary>
        /// <param name="color">
        /// The <see cref="Android.Graphics.Color"/> to darken.
        /// </param>
        /// <param name="amount">
        /// The amount to darken by.
        /// </param>
        /// <returns>
        /// Returns the darkened color as a <see cref="Android.Graphics.Color"/>.
        /// </returns>
        public static Color Darken(this Color color, float amount)
        {
            var val = amount * -.01;
            return Lerp(color, (float)val);
        }

        /// <summary>
        /// Checks whether the specified color is within the range of another by the specified amount.
        /// </summary>
        /// <param name="color">
        /// The color to compare.
        /// </param>
        /// <param name="comparer">
        /// The color to compare with.
        /// </param>
        /// <param name="amount">
        /// The range either side of the <paramref name="comparer"/>.
        /// </param>
        /// <returns>
        /// Returns true if is in range; else false.
        /// </returns>
        public static bool IsInRange(this Color color, Color comparer, float amount)
        {
            return IsInRange(color, comparer, amount, false);
        }

        /// <summary>
        /// Checks whether the specified color is within the range of another by the specified amount.
        /// </summary>
        /// <param name="color">
        /// The color to compare.
        /// </param>
        /// <param name="comparer">
        /// The color to compare with.
        /// </param>
        /// <param name="amount">
        /// The range either side of the <paramref name="comparer"/>.
        /// </param>
        /// <param name="ignoreTransparency">
        /// Indicates whether to ignore transparency.
        /// </param>
        /// <returns>
        /// Returns true if is in range; else false.
        /// </returns>
        public static bool IsInRange(this Color color, Color comparer, float amount, bool ignoreTransparency)
        {
            var isInRange = false;

            var colorToCompare = ignoreTransparency ? new Color(color.R, color.G, color.B, (byte)255) : color;

            for (var i = 0; i < amount; i++)
            {
                if (colorToCompare == comparer)
                {
                    isInRange = true;
                    break;
                }

                var darken = comparer.Darken(i);
                var lighten = comparer.Lighten(i);

                if (colorToCompare == darken)
                {
                    isInRange = true;
                    break;
                }

                if (colorToCompare == lighten)
                {
                    isInRange = true;
                    break;
                }
            }

            return isInRange;
        }

        /// <summary>
        /// Represents an AccentColor value as a dark Color value.
        /// </summary>
        /// <param name="accentColor">
        /// The accent color.
        /// </param>
        /// <returns>
        /// Returns a dark Color value for the given AccentColor.
        /// </returns>
        public static Color AsDarkAccentColor(this AccentColor accentColor)
        {
            var newColor = MaterialDesignColors.Indigo.Color700;

            switch (accentColor)
            {
                case AccentColor.Lime:
                    newColor = MaterialDesignColors.Lime.Color700;
                    break;
                case AccentColor.LightGreen:
                    newColor = MaterialDesignColors.LightGreen.Color700;
                    break;
                case AccentColor.Green:
                    newColor = MaterialDesignColors.Green.Color700;
                    break;
                case AccentColor.Teal:
                    newColor = MaterialDesignColors.Teal.Color700;
                    break;
                case AccentColor.Cyan:
                    newColor = MaterialDesignColors.Cyan.Color700;
                    break;
                case AccentColor.Blue:
                    newColor = MaterialDesignColors.Blue.Color700;
                    break;
                case AccentColor.DeepPurple:
                    newColor = MaterialDesignColors.DeepPurple.Color700;
                    break;
                case AccentColor.Pink:
                    newColor = MaterialDesignColors.Pink.Color700;
                    break;
                case AccentColor.Purple:
                    newColor = MaterialDesignColors.Purple.Color700;
                    break;
                case AccentColor.Red:
                    newColor = MaterialDesignColors.Red.Color700;
                    break;
                case AccentColor.Orange:
                    newColor = MaterialDesignColors.Orange.Color700;
                    break;
                case AccentColor.Amber:
                    newColor = MaterialDesignColors.Amber.Color700;
                    break;
                case AccentColor.Yellow:
                    newColor = MaterialDesignColors.Yellow.Color700;
                    break;
            }

            return newColor;
        }

        /// <summary>
        /// Represents an AccentColor value as a light Color value.
        /// </summary>
        /// <param name="accentColor">
        /// The accent color.
        /// </param>
        /// <returns>
        /// Returns a light Color value for the given AccentColor.
        /// </returns>
        public static Color AsLightAccentColor(this AccentColor accentColor)
        {
            var newColor = MaterialDesignColors.Indigo.Color100;

            switch (accentColor)
            {
                case AccentColor.Lime:
                    newColor = MaterialDesignColors.Lime.Color100;
                    break;
                case AccentColor.LightGreen:
                    newColor = MaterialDesignColors.LightGreen.Color100;
                    break;
                case AccentColor.Green:
                    newColor = MaterialDesignColors.Green.Color100;
                    break;
                case AccentColor.Teal:
                    newColor = MaterialDesignColors.Teal.Color100;
                    break;
                case AccentColor.Cyan:
                    newColor = MaterialDesignColors.Cyan.Color100;
                    break;
                case AccentColor.Blue:
                    newColor = MaterialDesignColors.Blue.Color100;
                    break;
                case AccentColor.DeepPurple:
                    newColor = MaterialDesignColors.DeepPurple.Color100;
                    break;
                case AccentColor.Pink:
                    newColor = MaterialDesignColors.Pink.Color100;
                    break;
                case AccentColor.Purple:
                    newColor = MaterialDesignColors.Purple.Color100;
                    break;
                case AccentColor.Red:
                    newColor = MaterialDesignColors.Red.Color100;
                    break;
                case AccentColor.Orange:
                    newColor = MaterialDesignColors.Orange.Color100;
                    break;
                case AccentColor.Amber:
                    newColor = MaterialDesignColors.Amber.Color100;
                    break;
                case AccentColor.Yellow:
                    newColor = MaterialDesignColors.Yellow.Color100;
                    break;
            }

            return newColor;
        }

        /// <summary>
        /// Represents an AccentColor value as a primary Color value.
        /// </summary>
        /// <param name="accentColor">
        /// The accent color.
        /// </param>
        /// <returns>
        /// Returns a primary Color for the given AccentColor.
        /// </returns>
        public static Color AsPrimaryAccentColor(this AccentColor accentColor)
        {
            var newColor = MaterialDesignColors.Indigo.Color500;

            switch (accentColor)
            {
                case AccentColor.Lime:
                    newColor = MaterialDesignColors.Lime.Color500;
                    break;
                case AccentColor.LightGreen:
                    newColor = MaterialDesignColors.LightGreen.Color500;
                    break;
                case AccentColor.Green:
                    newColor = MaterialDesignColors.Green.Color500;
                    break;
                case AccentColor.Teal:
                    newColor = MaterialDesignColors.Teal.Color500;
                    break;
                case AccentColor.Cyan:
                    newColor = MaterialDesignColors.Cyan.Color500;
                    break;
                case AccentColor.Blue:
                    newColor = MaterialDesignColors.Blue.Color500;
                    break;
                case AccentColor.DeepPurple:
                    newColor = MaterialDesignColors.DeepPurple.Color500;
                    break;
                case AccentColor.Pink:
                    newColor = MaterialDesignColors.Pink.Color500;
                    break;
                case AccentColor.Purple:
                    newColor = MaterialDesignColors.Purple.Color500;
                    break;
                case AccentColor.Red:
                    newColor = MaterialDesignColors.Red.Color500;
                    break;
                case AccentColor.Orange:
                    newColor = MaterialDesignColors.Orange.Color500;
                    break;
                case AccentColor.Amber:
                    newColor = MaterialDesignColors.Amber.Color500;
                    break;
                case AccentColor.Yellow:
                    newColor = MaterialDesignColors.Yellow.Color500;
                    break;
            }

            return newColor;
        }

        private static Color Lerp(this Color color, float amount)
        {
            float red = color.R;
            float green = color.G;
            float blue = color.B;

            if (amount < 0)
            {
                amount = 1 + amount;
                red *= amount;
                green *= amount;
                blue *= amount;
            }
            else
            {
                red = (255 - red) * amount + red;
                green = (255 - green) * amount + green;
                blue = (255 - blue) * amount + blue;
            }

            return new Color((byte)red, (byte)green, (byte)blue, color.A);
        }
    }
}