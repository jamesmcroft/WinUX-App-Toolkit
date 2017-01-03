namespace WinUX
{
    using Windows.UI.Xaml.Controls;

    using WinUX.Controls.ScrollViewer;

    /// <summary>
    /// Defines a collection of extensions for the ScrollViewer control.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Locks the specified <see cref="ScrollViewer"/> from being manipulated.
        /// </summary>
        /// <param name="scrollViewer">
        /// The <see cref="ScrollViewer"/>.
        /// </param>
        public static void Lock(this ScrollViewer scrollViewer)
        {
            if (scrollViewer != null)
            {
                scrollViewer.ZoomMode = ZoomMode.Disabled;
                scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                scrollViewer.HorizontalScrollMode = ScrollMode.Disabled;
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                scrollViewer.VerticalScrollMode = ScrollMode.Disabled;
            }
        }

        /// <summary>
        /// Updates the scroll mode for the specified <see cref="ScrollViewer"/>.
        /// </summary>
        /// <param name="scrollViewer">
        /// The <see cref="ScrollViewer"/>.
        /// </param>
        /// <param name="mode">
        /// The mode to set the <see cref="ScrollViewer"/>.
        /// </param>
        public static void UpdateScrollMode(this ScrollViewer scrollViewer, ScrollViewerMode mode)
        {
            if (scrollViewer != null)
            {
                switch (mode)
                {
                    case ScrollViewerMode.Horizontal:
                        scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                        scrollViewer.HorizontalScrollMode = ScrollMode.Auto;
                        scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                        scrollViewer.VerticalScrollMode = ScrollMode.Disabled;
                        break;
                    case ScrollViewerMode.Vertical:
                        scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                        scrollViewer.HorizontalScrollMode = ScrollMode.Disabled;
                        scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                        scrollViewer.VerticalScrollMode = ScrollMode.Auto;
                        break;
                    case ScrollViewerMode.All:
                        scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                        scrollViewer.HorizontalScrollMode = ScrollMode.Auto;
                        scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                        scrollViewer.VerticalScrollMode = ScrollMode.Auto;
                        break;
                    default:
                        scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                        scrollViewer.HorizontalScrollMode = ScrollMode.Disabled;
                        scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                        scrollViewer.VerticalScrollMode = ScrollMode.Disabled;
                        break;
                }
            }
        }
    }
}