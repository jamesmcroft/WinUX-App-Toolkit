namespace WinUX
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.Foundation;
    using Windows.Graphics.DirectX;
    using Windows.Graphics.Display;
    using Windows.Graphics.Imaging;
    using Windows.Storage;
    using Windows.UI.Input.Inking;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    using Microsoft.Graphics.Canvas;

    /// <summary>
    /// Defines a collection of extensions for handling ink.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Captures the ink from an <see cref="InkCanvas"/> control.
        /// </summary>
        /// <param name="strokeContainer">
        /// The <see cref="InkStrokeContainer"/> containing the strokes to be rendered.
        /// </param>
        /// <param name="rootRenderElement">
        /// A <see cref="FrameworkElement"/> which wraps the canvas.
        /// </param>
        /// <param name="targetFile">
        /// A <see cref="StorageFile"/> to store the image in.
        /// </param>
        /// <param name="encoderId">
        /// A <see cref="BitmapEncoder"/> ID to use to render the image.
        /// </param>
        /// <returns>
        /// Returns the <see cref="StorageFile"/> containing the rendered ink.
        /// </returns>
        public static async Task<StorageFile> CaptureInkToFileAsync(
            this InkStrokeContainer strokeContainer,
            FrameworkElement rootRenderElement,
            StorageFile targetFile,
            Guid encoderId)
        {
            if (targetFile != null)
            {
                var renderBitmap = new RenderTargetBitmap();
                await renderBitmap.RenderAsync(rootRenderElement);

                var bitmapSizeAt96Dpi = new Size(renderBitmap.PixelWidth, renderBitmap.PixelHeight);

                var pixels = await renderBitmap.GetPixelsAsync();

                var win2DDevice = CanvasDevice.GetSharedDevice();

                using (
                    var target = new CanvasRenderTarget(
                        win2DDevice,
                        (float)rootRenderElement.ActualWidth,
                        (float)rootRenderElement.ActualHeight,
                        96.0f))
                {
                    using (var drawingSession = target.CreateDrawingSession())
                    {
                        using (
                            var canvasBitmap = CanvasBitmap.CreateFromBytes(
                                win2DDevice,
                                pixels,
                                (int)bitmapSizeAt96Dpi.Width,
                                (int)bitmapSizeAt96Dpi.Height,
                                DirectXPixelFormat.B8G8R8A8UIntNormalized,
                                96.0f))
                        {
                            drawingSession.DrawImage(
                                canvasBitmap,
                                new Rect(0, 0, target.SizeInPixels.Width, target.SizeInPixels.Height),
                                new Rect(0, 0, bitmapSizeAt96Dpi.Width, bitmapSizeAt96Dpi.Height));
                        }
                        drawingSession.Units = CanvasUnits.Pixels;
                        drawingSession.DrawInk(strokeContainer.GetStrokes());
                    }

                    using (var stream = await targetFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        var displayInfo = DisplayInformation.GetForCurrentView();
                        var encoder = await BitmapEncoder.CreateAsync(encoderId, stream);
                        encoder.SetPixelData(
                            BitmapPixelFormat.Bgra8,
                            BitmapAlphaMode.Ignore,
                            target.SizeInPixels.Width,
                            target.SizeInPixels.Height,
                            displayInfo.RawDpiX,
                            displayInfo.RawDpiY,
                            target.GetPixelBytes());

                        await encoder.FlushAsync();
                    }
                }
            }

            return targetFile;
        }

        /// <summary>
        /// Captures the ink from an <see cref="InkCanvas"/> control.
        /// </summary>
        /// <param name="canvas">
        /// The <see cref="InkCanvas"/> control.
        /// </param>
        /// <param name="rootRenderElement">
        /// A <see cref="FrameworkElement"/> which wraps the canvas.
        /// </param>
        /// <param name="targetFile">
        /// A <see cref="StorageFile"/> to store the image in.
        /// </param>
        /// <param name="encoderId">
        /// A <see cref="BitmapEncoder"/> ID to use to render the image.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<StorageFile> CaptureInkToFileAsync(
            this InkCanvas canvas,
            FrameworkElement rootRenderElement,
            StorageFile targetFile,
            Guid encoderId)
        {
            return canvas != null
                       ? await
                         canvas.InkPresenter.StrokeContainer.CaptureInkToFileAsync(
                             rootRenderElement,
                             targetFile,
                             encoderId)
                       : targetFile;
        }

        /// <summary>
        /// Checks whether any strokes are selected.
        /// </summary>
        /// <param name="inkManager">
        /// The <see cref="InkManager"/>.
        /// </param>
        /// <returns>
        /// Returns true if any strokes are selected; else false.
        /// </returns>
        public static bool AnyStrokesSelected(this InkManager inkManager)
        {
            return inkManager.GetStrokes().Any(stroke => stroke.Selected);
        }

        /// <summary>
        /// Clears the selection of any stroke.
        /// </summary>
        /// <param name="inkManager">
        /// The <see cref="InkManager"/>.
        /// </param>
        public static void ClearStrokeSelection(this InkManager inkManager)
        {
            foreach (var stroke in inkManager.GetStrokes())
            {
                stroke.Selected = false;
            }
        }

        /// <summary>
        /// Selects all of the strokes.
        /// </summary>
        /// <param name="inkManager">
        /// The <see cref="InkManager"/>.
        /// </param>
        public static void SelectAllStrokes(this InkManager inkManager)
        {
            foreach (var stroke in inkManager.GetStrokes())
            {
                stroke.Selected = true;
            }
        }

        /// <summary>
        /// Clears all of the strokes.
        /// </summary>
        /// <param name="inkManager">
        /// The <see cref="InkManager"/>.
        /// </param>
        public static void ClearAllStrokes(this InkManager inkManager)
        {
            inkManager.SelectAllStrokes();
            inkManager.DeleteSelected();
        }

        /// <summary>
        /// Clones the ink strokes of the specified <see cref="InkManager"/>.
        /// </summary>
        /// <param name="inkManager">
        /// The <see cref="InkManager"/>.
        /// </param>
        /// <returns>
        /// Returns a collection of cloned strokes.
        /// </returns>
        public static IEnumerable<InkStroke> CloneInkStrokes(this InkManager inkManager)
        {
            return inkManager.GetStrokes().Select(stroke => stroke.Clone()).ToList();
        }

        /// <summary>
        /// Adds a collection of strokes to the specified <see cref="InkManager"/>.
        /// </summary>
        /// <param name="inkManager">
        /// The <see cref="InkManager"/>.
        /// </param>
        /// <param name="strokes">
        /// The strokes to add.
        /// </param>
        public static void AddStrokes(this InkManager inkManager, IEnumerable<InkStroke> strokes)
        {
            foreach (var stroke in strokes)
            {
                inkManager.AddStroke(stroke);
            }
        }

        /// <summary>
        /// Gets the bounding rectangle of a collection of ink strokes.
        /// </summary>
        /// <param name="inkStrokes">
        /// The ink strokes.
        /// </param>
        /// <returns>
        /// Returns the <see cref="Windows.Foundation.Rect"/> containing the ink strokes.
        /// </returns>
        public static Rect GetBoundingRect(this IEnumerable<InkStroke> inkStrokes)
        {
            var strokes = inkStrokes as IList<InkStroke> ?? inkStrokes.ToList();

            var topMost = strokes.GetTopLeftMostInkPoint();
            var bottomMost = strokes.GetBottomRightMostInkPoint();

            return new Rect(topMost, bottomMost);
        }

        /// <summary>
        /// Gets the top left most point from a collection of ink strokes.
        /// </summary>
        /// <param name="inkStrokes">
        /// The ink strokes.
        /// </param>
        /// <returns>
        /// The top left most <see cref="Windows.Foundation.Point"/>.
        /// </returns>
        internal static Point GetTopLeftMostInkPoint(this IEnumerable<InkStroke> inkStrokes)
        {
            var point = new Point();

            var strokes = inkStrokes as IList<InkStroke> ?? inkStrokes.ToList();

            var first = strokes.FirstOrDefault();
            if (first != null)
            {
                point = first.GetInkPoints().GetTopLeftMostInkPoint();
            }

            foreach (var inkStroke in strokes)
            {
                var strokePoint = inkStroke.GetInkPoints().GetTopLeftMostInkPoint();

                if (strokePoint.X < point.X)
                {
                    point.X = strokePoint.X;
                }

                if (strokePoint.Y < point.Y)
                {
                    point.Y = strokePoint.Y;
                }
            }

            return point;
        }

        /// <summary>
        /// Gets the top left most point from a collection of ink points.
        /// </summary>
        /// <param name="inkPoints">
        /// The ink points.
        /// </param>
        /// <returns>
        /// The top left most <see cref="Windows.Foundation.Point"/>.
        /// </returns>
        internal static Point GetTopLeftMostInkPoint(this IEnumerable<InkPoint> inkPoints)
        {
            var point = new Point(0, 0);

            var ips = inkPoints as IList<InkPoint> ?? inkPoints.ToList();

            var first = ips.FirstOrDefault();
            if (first != null)
            {
                point = first.Position;
            }

            foreach (var ip in ips)
            {
                if (ip.Position.X < point.X)
                {
                    point.X = ip.Position.X;
                }

                if (ip.Position.Y < point.Y)
                {
                    point.Y = ip.Position.Y;
                }
            }

            return point;
        }

        /// <summary>
        /// Gets the bottom right most point from a collection of ink strokes.
        /// </summary>
        /// <param name="inkStrokes">
        /// The ink strokes.
        /// </param>
        /// <returns>
        /// The bottom right most <see cref="Windows.Foundation.Point"/>.
        /// </returns>
        internal static Point GetBottomRightMostInkPoint(this IEnumerable<InkStroke> inkStrokes)
        {
            var point = new Point();

            var strokes = inkStrokes as IList<InkStroke> ?? inkStrokes.ToList();

            var first = strokes.FirstOrDefault();
            if (first != null)
            {
                point = first.GetInkPoints().GetBottomRightMostInkPoint();
            }

            foreach (var inkStroke in strokes)
            {
                var strokePoint = inkStroke.GetInkPoints().GetBottomRightMostInkPoint();

                if (strokePoint.X > point.X)
                {
                    point.X = strokePoint.X;
                }

                if (strokePoint.Y > point.Y)
                {
                    point.Y = strokePoint.Y;
                }
            }

            return point;
        }

        /// <summary>
        /// Gets the bottom right most point from a collection of ink points.
        /// </summary>
        /// <param name="inkPoints">
        /// The ink points.
        /// </param>
        /// <returns>
        /// The bottom right most <see cref="Windows.Foundation.Point"/>.
        /// </returns>
        internal static Point GetBottomRightMostInkPoint(this IEnumerable<InkPoint> inkPoints)
        {
            var point = new Point(0, 0);

            var ips = inkPoints as IList<InkPoint> ?? inkPoints.ToList();

            var first = ips.FirstOrDefault();
            if (first != null)
            {
                point = first.Position;
            }

            foreach (var ip in ips)
            {
                if (ip.Position.X > point.X)
                {
                    point.X = ip.Position.X;
                }

                if (ip.Position.Y > point.Y)
                {
                    point.Y = ip.Position.Y;
                }
            }

            return point;
        }
    }
}