namespace WinUX.Maths
{
    using System.Runtime.InteropServices;

    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a struct for holding an orientation and associated width and height.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct OrientedSize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrientedSize"/> struct.
        /// </summary>
        /// <param name="orientation">
        /// The orientation.
        /// </param>
        public OrientedSize(Orientation orientation)
            : this(orientation, 0.0, 0.0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrientedSize"/> struct.
        /// </summary>
        /// <param name="orientation">
        /// The orientation.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        public OrientedSize(Orientation orientation, double width, double height)
        {
            this.Orientation = orientation;

            this.Direct = 0.0;
            this.Indirect = 0.0;

            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets the orientation.
        /// </summary>
        public Orientation Orientation { get; }

        /// <summary>
        /// Gets or sets the size dimension that grows directly with layout placement.
        /// </summary>
        public double Direct { get; set; }

        /// <summary>
        /// Gets or sets the size dimension that grows indirectly with the maximum value of the layout row or column.
        /// </summary>
        public double Indirect { get; set; }

        /// <summary>
        /// Gets or sets the width of the size.
        /// </summary>
        public double Width
        {
            get
            {
                return this.Orientation == Orientation.Horizontal ? this.Direct : this.Indirect;
            }
            set
            {
                if (this.Orientation == Orientation.Horizontal)
                {
                    this.Direct = value;
                }
                else
                {
                    this.Indirect = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the size.
        /// </summary>
        public double Height
        {
            get
            {
                return this.Orientation != Orientation.Horizontal ? this.Direct : this.Indirect;
            }
            set
            {
                if (this.Orientation != Orientation.Horizontal)
                {
                    this.Direct = value;
                }
                else
                {
                    this.Indirect = value;
                }
            }
        }
    }
}