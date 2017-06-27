namespace WinUX.Design.Material
{
    using Android.Graphics;

    using WinUX.Design.Material.ColorSwatches;

    /// <summary>
    /// Defines the collection of material design color swatches.
    /// </summary>
    /// <remarks>
    /// More information can be found here: https://material.google.com/style/color.html
    /// </remarks>
    public sealed class MaterialDesignColors
    {
        private static IMaterialColorSwatch<Color> redSwatch;

        private static IMaterialColorSwatch<Color> pinkSwatch;

        private static IMaterialColorSwatch<Color> purpleSwatch;

        private static IMaterialColorSwatch<Color> deepPurpleSwatch;

        private static IMaterialColorSwatch<Color> indigoSwatch;

        private static IMaterialColorSwatch<Color> blueSwatch;

        private static IMaterialColorSwatch<Color> lightBlueSwatch;

        private static IMaterialColorSwatch<Color> cyanSwatch;

        private static IMaterialColorSwatch<Color> tealSwatch;

        private static IMaterialColorSwatch<Color> greenSwatch;

        private static IMaterialColorSwatch<Color> blueGreySwatch;

        private static IMaterialColorSwatch<Color> greySwatch;

        private static IMaterialColorSwatch<Color> lightGreenSwatch;

        private static IMaterialColorSwatch<Color> limeSwatch;

        private static IMaterialColorSwatch<Color> yellowSwatch;

        private static IMaterialColorSwatch<Color> brownSwatch;

        private static IMaterialColorSwatch<Color> amberSwatch;

        private static IMaterialColorSwatch<Color> orangeSwatch;

        private static IMaterialColorSwatch<Color> deepOrangeSwatch;

        /// <summary>
        /// Gets the red color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Red => redSwatch ?? (redSwatch = new RedColorSwatch());

        /// <summary>
        /// Gets the pink color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Pink => pinkSwatch ?? (pinkSwatch = new PinkColorSwatch());

        /// <summary>
        /// Gets the purple color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Purple => purpleSwatch ?? (purpleSwatch = new PurpleColorSwatch());

        /// <summary>
        /// Gets the deep purple color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> DeepPurple
            => deepPurpleSwatch ?? (deepPurpleSwatch = new DeepPurpleColorSwatch());

        /// <summary>
        /// Gets the indigo color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Indigo => indigoSwatch ?? (indigoSwatch = new IndigoColorSwatch());

        /// <summary>
        /// Gets the blue color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Blue => blueSwatch ?? (blueSwatch = new BlueColorSwatch());

        /// <summary>
        /// Gets the light blue color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> LightBlue
            => lightBlueSwatch ?? (lightBlueSwatch = new LightBlueColorSwatch());

        /// <summary>
        /// Gets the cyan color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Cyan => cyanSwatch ?? (cyanSwatch = new CyanColorSwatch());

        /// <summary>
        /// Gets the teal color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Teal => tealSwatch ?? (tealSwatch = new TealColorSwatch());

        /// <summary>
        /// Gets the green color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Green => greenSwatch ?? (greenSwatch = new GreenColorSwatch());

        /// <summary>
        /// Gets the light green color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> LightGreen
            => lightGreenSwatch ?? (lightGreenSwatch = new LightGreenColorSwatch());

        /// <summary>
        /// Gets the lime color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Lime => limeSwatch ?? (limeSwatch = new LimeColorSwatch());

        /// <summary>
        /// Gets the yellow color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Yellow => yellowSwatch ?? (yellowSwatch = new YellowColorSwatch());

        /// <summary>
        /// Gets the amber color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Amber => amberSwatch ?? (amberSwatch = new AmberColorSwatch());

        /// <summary>
        /// Gets the orange color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Orange => orangeSwatch ?? (orangeSwatch = new OrangeColorSwatch());

        /// <summary>
        /// Gets the deep orange color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> DeepOrange
            => deepOrangeSwatch ?? (deepOrangeSwatch = new DeepOrangeColorSwatch());

        /// <summary>
        /// Gets the brown color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Brown => brownSwatch ?? (brownSwatch = new BrownColorSwatch());

        /// <summary>
        /// Gets the grey color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> Grey => greySwatch ?? (greySwatch = new GreyColorSwatch());

        /// <summary>
        /// Gets the blue grey color swatch.
        /// </summary>
        public static IMaterialColorSwatch<Color> BlueGrey
            => blueGreySwatch ?? (blueGreySwatch = new BlueGreyColorSwatch());
    }
}