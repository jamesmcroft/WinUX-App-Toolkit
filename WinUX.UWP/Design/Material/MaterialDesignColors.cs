namespace WinUX.UWP.Design.Material
{
    using WinUX.UWP.Design.Material.ColorSwatches;

    /// <summary>
    /// Defines the collection of material design color swatches.
    /// </summary>
    /// <remarks>
    /// More information can be found here: https://material.google.com/style/color.html
    /// </remarks>
    public sealed class MaterialDesignColors
    {
        private static IMaterialColorSwatch redSwatch;

        private static IMaterialColorSwatch pinkSwatch;

        private static IMaterialColorSwatch purpleSwatch;

        private static IMaterialColorSwatch deepPurpleSwatch;

        private static IMaterialColorSwatch indigoSwatch;

        private static IMaterialColorSwatch blueSwatch;

        private static IMaterialColorSwatch lightBlueSwatch;

        private static IMaterialColorSwatch cyanSwatch;

        private static IMaterialColorSwatch tealSwatch;

        private static IMaterialColorSwatch greenSwatch;

        private static IMaterialColorSwatch blueGreySwatch;

        private static IMaterialColorSwatch greySwatch;

        private static IMaterialColorSwatch lightGreenSwatch;

        private static IMaterialColorSwatch limeSwatch;

        private static IMaterialColorSwatch yellowSwatch;

        private static IMaterialColorSwatch brownSwatch;

        private static IMaterialColorSwatch amberSwatch;

        private static IMaterialColorSwatch orangeSwatch;

        private static IMaterialColorSwatch deepOrangeSwatch;

        /// <summary>
        /// Gets the red color swatch.
        /// </summary>
        public static IMaterialColorSwatch Red => redSwatch ?? (redSwatch = new RedColorSwatch());

        /// <summary>
        /// Gets the pink color swatch.
        /// </summary>
        public static IMaterialColorSwatch Pink => pinkSwatch ?? (pinkSwatch = new PinkColorSwatch());

        /// <summary>
        /// Gets the purple color swatch.
        /// </summary>
        public static IMaterialColorSwatch Purple => purpleSwatch ?? (purpleSwatch = new PurpleColorSwatch());

        /// <summary>
        /// Gets the deep purple color swatch.
        /// </summary>
        public static IMaterialColorSwatch DeepPurple
            => deepPurpleSwatch ?? (deepPurpleSwatch = new DeepPurpleColorSwatch());

        /// <summary>
        /// Gets the indigo color swatch.
        /// </summary>
        public static IMaterialColorSwatch Indigo => indigoSwatch ?? (indigoSwatch = new IndigoColorSwatch());

        /// <summary>
        /// Gets the blue color swatch.
        /// </summary>
        public static IMaterialColorSwatch Blue => blueSwatch ?? (blueSwatch = new BlueColorSwatch());

        /// <summary>
        /// Gets the light blue color swatch.
        /// </summary>
        public static IMaterialColorSwatch LightBlue
            => lightBlueSwatch ?? (lightBlueSwatch = new LightBlueColorSwatch());

        /// <summary>
        /// Gets the cyan color swatch.
        /// </summary>
        public static IMaterialColorSwatch Cyan => cyanSwatch ?? (cyanSwatch = new CyanColorSwatch());

        /// <summary>
        /// Gets the teal color swatch.
        /// </summary>
        public static IMaterialColorSwatch Teal => tealSwatch ?? (tealSwatch = new TealColorSwatch());

        /// <summary>
        /// Gets the green color swatch.
        /// </summary>
        public static IMaterialColorSwatch Green => greenSwatch ?? (greenSwatch = new GreenColorSwatch());

        /// <summary>
        /// Gets the light green color swatch.
        /// </summary>
        public static IMaterialColorSwatch LightGreen
            => lightGreenSwatch ?? (lightGreenSwatch = new LightGreenColorSwatch());

        /// <summary>
        /// Gets the lime color swatch.
        /// </summary>
        public static IMaterialColorSwatch Lime => limeSwatch ?? (limeSwatch = new LimeColorSwatch());

        /// <summary>
        /// Gets the yellow color swatch.
        /// </summary>
        public static IMaterialColorSwatch Yellow => yellowSwatch ?? (yellowSwatch = new YellowColorSwatch());

        /// <summary>
        /// Gets the amber color swatch.
        /// </summary>
        public static IMaterialColorSwatch Amber => amberSwatch ?? (amberSwatch = new AmberColorSwatch());

        /// <summary>
        /// Gets the orange color swatch.
        /// </summary>
        public static IMaterialColorSwatch Orange => orangeSwatch ?? (orangeSwatch = new OrangeColorSwatch());

        /// <summary>
        /// Gets the deep orange color swatch.
        /// </summary>
        public static IMaterialColorSwatch DeepOrange
            => deepOrangeSwatch ?? (deepOrangeSwatch = new DeepOrangeColorSwatch());

        /// <summary>
        /// Gets the brown color swatch.
        /// </summary>
        public static IMaterialColorSwatch Brown => brownSwatch ?? (brownSwatch = new BrownColorSwatch());

        /// <summary>
        /// Gets the grey color swatch.
        /// </summary>
        public static IMaterialColorSwatch Grey => greySwatch ?? (greySwatch = new GreyColorSwatch());

        /// <summary>
        /// Gets the blue grey color swatch.
        /// </summary>
        public static IMaterialColorSwatch BlueGrey
            => blueGreySwatch ?? (blueGreySwatch = new BlueGreyColorSwatch());
    }
}