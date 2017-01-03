namespace WinUX.Xaml.Controls
{
    using System;

    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using WinUX.Maths;

    /// <summary>
    /// Defines a panel that supports wrapping content in a view.
    /// </summary>
    public sealed partial class WrapPanel : Panel
    {
        private bool ignorePropertyChange;

        /// <summary>
        /// Measures the child elements of the panel.
        /// </summary>
        /// <param name="constraint">
        /// The size available for child elements.
        /// </param>
        /// <returns>
        /// Returns the size required by the panel.
        /// </returns>
        protected override Size MeasureOverride(Size constraint)
        {
            var o = this.Orientation;
            var lineSize = new OrientedSize(o);
            var totalSize = new OrientedSize(o);
            var maximumSize = new OrientedSize(o, constraint.Width, constraint.Height);

            // Determine the constraints for individual items
            double itemWidth = this.ItemWidth;
            double itemHeight = this.ItemHeight;
            bool hasFixedWidth = !double.IsNaN(itemWidth);
            bool hasFixedHeight = !double.IsNaN(itemHeight);
            var itemSize = new Size(
                hasFixedWidth ? itemWidth : constraint.Width,
                hasFixedHeight ? itemHeight : constraint.Height);

            // Measure each of the children
            foreach (var element in this.Children)
            {
                // Determine the size of the element
                element.Measure(itemSize);
                var elementSize = new OrientedSize(
                    o,
                    hasFixedWidth ? itemWidth : element.DesiredSize.Width,
                    hasFixedHeight ? itemHeight : element.DesiredSize.Height);

                // If this element falls of the edge of the line
                if (MathHelper.IsGreaterThan(lineSize.Direct + elementSize.Direct, maximumSize.Direct))
                {
                    // Update the total size with the direct and indirect growth for the current line
                    totalSize.Direct = Math.Max(lineSize.Direct, totalSize.Direct);
                    totalSize.Indirect += lineSize.Indirect;

                    // Move the element to a new line
                    lineSize = elementSize;

                    // If the current element is larger than the maximum size, place it on a line by itself
                    if (MathHelper.IsGreaterThan(elementSize.Direct, maximumSize.Direct))
                    {
                        // Update the total size for the line occupied by this single element
                        totalSize.Direct = Math.Max(elementSize.Direct, totalSize.Direct);
                        totalSize.Indirect += elementSize.Indirect;

                        // Move to a new line
                        lineSize = new OrientedSize(o);
                    }
                }
                else
                {
                    // Otherwise just add the element to the end of the line
                    lineSize.Direct += elementSize.Direct;
                    lineSize.Indirect = Math.Max(lineSize.Indirect, elementSize.Indirect);
                }
            }

            // Update the total size with the elements on the last line
            totalSize.Direct = Math.Max(lineSize.Direct, totalSize.Direct);
            totalSize.Indirect += lineSize.Indirect;

            // Return the total size required as an un-oriented quantity
            return new Size(totalSize.Width, totalSize.Height);
        }

        /// <summary>
        /// Arranges the child elements of the panel.
        /// </summary>
        /// <param name="finalSize">
        /// The final size.
        /// </param>
        /// <returns>
        /// Returns the size required by the panel.
        /// </returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var o = this.Orientation;
            var lineSize = new OrientedSize(o);
            var maximumSize = new OrientedSize(o, finalSize.Width, finalSize.Height);

            // Determine the constraints for individual items
            double itemWidth = this.ItemWidth;
            double itemHeight = this.ItemHeight;
            bool hasFixedWidth = !itemWidth.IsNaN();
            bool hasFixedHeight = !itemHeight.IsNaN();
            double indirectOffset = 0;
            var directDelta = (o == Orientation.Horizontal)
                                  ? (hasFixedWidth ? (double?)itemWidth : null)
                                  : (hasFixedHeight ? (double?)itemHeight : null);

            var children = this.Children;
            int count = children.Count;
            int lineStart = 0;
            for (var lineEnd = 0; lineEnd < count; lineEnd++)
            {
                var element = children[lineEnd];

                // Get the size of the element
                var elementSize = new OrientedSize(
                    o,
                    hasFixedWidth ? itemWidth : element.DesiredSize.Width,
                    hasFixedHeight ? itemHeight : element.DesiredSize.Height);

                // If this element falls of the edge of the line
                if (MathHelper.IsGreaterThan(lineSize.Direct + elementSize.Direct, maximumSize.Direct))
                {
                    // Then we just completed a line and we should arrange it
                    this.ArrangeLine(lineStart, lineEnd, directDelta, indirectOffset, lineSize.Indirect);

                    // Move the current element to a new line
                    indirectOffset += lineSize.Indirect;
                    lineSize = elementSize;

                    // If the current element is larger than the maximum size
                    if (MathHelper.IsGreaterThan(elementSize.Direct, maximumSize.Direct))
                    {
                        // Arrange the element as a single line
                        this.ArrangeLine(lineEnd, ++lineEnd, directDelta, indirectOffset, elementSize.Indirect);

                        // Move to a new line
                        indirectOffset += lineSize.Indirect;
                        lineSize = new OrientedSize(o);
                    }

                    // Advance the start index to a new line after arranging
                    lineStart = lineEnd;
                }
                else
                {
                    // Otherwise just add the element to the end of the line
                    lineSize.Direct += elementSize.Direct;
                    lineSize.Indirect = Math.Max(lineSize.Indirect, elementSize.Indirect);
                }
            }

            // Arrange any elements on the last line
            if (lineStart < count)
            {
                this.ArrangeLine(lineStart, count, directDelta, indirectOffset, lineSize.Indirect);
            }

            return finalSize;
        }

        private void ArrangeLine(
            int lineStart,
            int lineEnd,
            double? directDelta,
            double indirectOffset,
            double indirectGrowth)
        {
            double directOffset = 0.0;

            var o = this.Orientation;
            bool isHorizontal = o == Orientation.Horizontal;

            var children = this.Children;
            for (var index = lineStart; index < lineEnd; index++)
            {
                // Get the size of the element
                var element = children[index];
                var elementSize = new OrientedSize(o, element.DesiredSize.Width, element.DesiredSize.Height);

                // Determine if we should use the element's desired size or the fixed item width or height
                double directGrowth = directDelta ?? elementSize.Direct;

                // Arrange the element
                var bounds = isHorizontal
                                 ? new Rect(directOffset, indirectOffset, directGrowth, indirectGrowth)
                                 : new Rect(indirectOffset, directOffset, indirectGrowth, directGrowth);

                element.Arrange(bounds);

                directOffset += directGrowth;
            }
        }

        private void OnOrientationChanged(DependencyPropertyChangedEventArgs e)
        {
            var value = (Orientation)e.NewValue;

            if (this.ignorePropertyChange)
            {
                this.ignorePropertyChange = false;
                return;
            }

            if ((value != Orientation.Horizontal) && (value != Orientation.Vertical))
            {
                this.ignorePropertyChange = true;
                this.SetValue(WrapPanel.OrientationProperty, (Orientation)e.OldValue);

                return;
            }

            this.InvalidateMeasure();
        }

        private void OnItemSizeChanged(DependencyPropertyChangedEventArgs e)
        {
            double value = (double)e.NewValue;

            if (this.ignorePropertyChange)
            {
                this.ignorePropertyChange = false;
                return;
            }

            if (!double.IsNaN(value) && ((value <= 0.0) || double.IsPositiveInfinity(value)))
            {
                this.ignorePropertyChange = true;
                this.SetValue(e.Property, (double)e.OldValue);

                return;
            }

            this.InvalidateMeasure();
        }
    }
}