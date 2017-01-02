namespace WinUX.Attributes
{
    using System;

    /// <summary>
    /// Defines an attribute for providing a string based description.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">
        /// The description of the attributed object.
        /// </param>
        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// Gets or sets the description of the attributed object.
        /// </summary>
        public string Description { get; set; }
    }
}