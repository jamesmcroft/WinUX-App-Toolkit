namespace WinUX.Xaml.Controls
{
    using System;

    using Windows.Foundation;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a panel for displaying full-width content in a view.
    /// </summary>
    public sealed partial class UniformWidthPanel : Panel
    {
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
            var finalSize = new Size { Width = constraint.Width };
            var columnWidth = constraint.Width / this.MaximumColumns;

            var rowHeight = 0d;
            var rowChildCount = 0;
            foreach (var child in this.Children)
            {
                child.Measure(new Size(columnWidth, constraint.Height));
                if (rowChildCount < this.MaximumColumns)
                {
                    rowHeight = Math.Max(child.DesiredSize.Height, rowHeight);
                }
                else
                {
                    finalSize.Height += rowHeight;
                    rowHeight = child.DesiredSize.Height;
                    rowChildCount = 0;
                }
                rowChildCount++;
            }

            finalSize.Height += rowHeight;
            return finalSize;
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
            var columnWidth = finalSize.Width / this.MaximumColumns;
            var posY = 0d;

            var rowHeight = 0d;
            var rowChildCount = 0;
            foreach (var child in this.Children)
            {
                if (rowChildCount >= this.MaximumColumns)
                {
                    rowChildCount = 0;
                    posY += rowHeight;
                    rowHeight = 0;
                }

                child.Arrange(new Rect(columnWidth * rowChildCount, posY, columnWidth, child.DesiredSize.Height));
                rowHeight = Math.Max(child.DesiredSize.Height, rowHeight);
                rowChildCount++;
            }

            return finalSize;
        }
    }
}