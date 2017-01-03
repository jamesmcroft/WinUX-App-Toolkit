namespace WinUX.UWP.Samples.Components
{
    public class SampleProperty
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the code used for the binding of the property.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the type of property.
        /// </summary>
        public SamplePropertyType Type { get; set; }

        /// <summary>
        /// Gets or sets the default value for the property.
        /// </summary>
        public object DefaultValue { get; set; }
    }
}
