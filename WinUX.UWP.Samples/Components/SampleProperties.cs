namespace WinUX.UWP.Samples.Components
{
    using System.Dynamic;
    using System.Collections.Generic;

    public sealed class SampleProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleProperties"/> class.
        /// </summary>
        public SampleProperties()
        {
            this.Properties = new List<SampleProperty>();
            this.Bindings = new ExpandoObject();
        }

        /// <summary>
        /// Gets the properties to bind to.
        /// </summary>
        public List<SampleProperty> Properties { get; private set; }

        /// <summary>
        /// Gets or sets the bindings for the properties.
        /// </summary>
        public ExpandoObject Bindings { get; set; }
    }
}