namespace WinUX.Xaml.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Graphics.Imaging;
    using Windows.Storage;
    using Windows.UI.Core;
    using Windows.UI.Input.Inking;
    using Windows.UI.Input.Inking.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using WinUX.Input.Inking;
    using WinUX.Threading;
    using WinUX.Xaml;

    /// <summary>
    /// Defines a control for providing better InkCanvas saving functionality.
    /// </summary>
    [TemplatePart(Name = "RenderBackground", Type = typeof(Grid))]
    [TemplatePart(Name = "DrawingArea", Type = typeof(InkCanvas))]
    public sealed partial class DrawingCanvas : Control
    {
        private readonly Guid instanceIdentifier;

        private readonly SemaphoreSlim fileSaveSemaphore = new SemaphoreSlim(1);

        private Grid renderBackground;

        private CoreInkIndependentInputSource inkInputSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawingCanvas"/> class.
        /// </summary>
        public DrawingCanvas()
        {
            this.DefaultStyleKey = typeof(DrawingCanvas);

            this.instanceIdentifier = Guid.NewGuid();
        }

        /// <summary>
        /// Called when applying the control's template.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UIDispatcher.Initialize();

            if (this.inkInputSource != null)
            {
                this.inkInputSource.PointerEntering -= this.OnInkPointerEntering;
                this.inkInputSource.PointerExiting -= this.OnInkPointerExiting;
                this.inkInputSource.PointerHovering -= this.OnInkPointerHovering;
                this.inkInputSource.PointerLost -= this.OnInkPointerLost;
                this.inkInputSource.PointerMoving -= this.OnInkPointerMoving;
                this.inkInputSource.PointerPressing -= this.OnInkPointerPressing;
                this.inkInputSource.PointerReleasing -= this.OnInkPointerReleasing;
                this.inkInputSource = null;
            }

            if (this.InkCanvas != null)
            {
                this.InkCanvas.InkPresenter.StrokesCollected -= this.DrawingArea_OnStrokesCollected;
                this.InkCanvas.InkPresenter.StrokesErased -= this.DrawingArea_OnStrokesErased;
                this.InkCanvas = null;
            }

            this.renderBackground = this.GetTemplateChild("RenderBackground") as Grid;
            this.InkCanvas = this.GetTemplateChild("DrawingArea") as InkCanvas;

            if (this.InkCanvas != null)
            {
                this.inkInputSource = CoreInkIndependentInputSource.Create(this.InkCanvas.InkPresenter);
                this.inkInputSource.PointerEntering += this.OnInkPointerEntering;
                this.inkInputSource.PointerExiting += this.OnInkPointerExiting;
                this.inkInputSource.PointerHovering += this.OnInkPointerHovering;
                this.inkInputSource.PointerLost += this.OnInkPointerLost;
                this.inkInputSource.PointerMoving += this.OnInkPointerMoving;
                this.inkInputSource.PointerPressing += this.OnInkPointerPressing;
                this.inkInputSource.PointerReleasing += this.OnInkPointerReleasing;

                this.InkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Mouse
                                                               | CoreInputDeviceTypes.Touch;

                this.InkCanvas.InkPresenter.StrokesCollected += this.DrawingArea_OnStrokesCollected;
                this.InkCanvas.InkPresenter.StrokesErased += this.DrawingArea_OnStrokesErased;
            }

            this.UpdateDrawingAttributes(this.DrawingAttributes);
        }

        private void OnInkPointerReleasing(CoreInkIndependentInputSource sender, PointerEventArgs args)
        {
            SynchronizationContextHelper.Execute(
                this.InkPointerReleasing,
                this,
                new InkPointerEventArgs(args.CurrentPoint.GetPointerId(), args));
        }

        private void OnInkPointerMoving(CoreInkIndependentInputSource sender, PointerEventArgs args)
        {
            SynchronizationContextHelper.Execute(
                this.InkPointerMoving,
                this,
                new InkPointerEventArgs(args.CurrentPoint.GetPointerId(), args));
        }

        private void OnInkPointerLost(CoreInkIndependentInputSource sender, PointerEventArgs args)
        {
            SynchronizationContextHelper.Execute(
                this.InkPointerLost,
                this,
                new InkPointerEventArgs(args.CurrentPoint.GetPointerId(), args));
        }

        private void OnInkPointerHovering(CoreInkIndependentInputSource sender, PointerEventArgs args)
        {
            SynchronizationContextHelper.Execute(
                this.InkPointerHovering,
                this,
                new InkPointerEventArgs(args.CurrentPoint.GetPointerId(), args));
        }

        private void OnInkPointerExiting(CoreInkIndependentInputSource sender, PointerEventArgs args)
        {
            SynchronizationContextHelper.Execute(
                this.InkPointerExiting,
                this,
                new InkPointerEventArgs(args.CurrentPoint.GetPointerId(), args));
        }

        private void OnInkPointerEntering(CoreInkIndependentInputSource sender, PointerEventArgs args)
        {
            SynchronizationContextHelper.Execute(
                this.InkPointerEntering,
                this,
                new InkPointerEventArgs(args.CurrentPoint.GetPointerId(), args));
        }

        private void OnInkPointerPressing(CoreInkIndependentInputSource sender, PointerEventArgs args)
        {
            SynchronizationContextHelper.Execute(
                this.InkPointerPressing,
                this,
                new InkPointerEventArgs(args.CurrentPoint.GetPointerId(), args));
        }

        private void UpdateDrawingAttributes(InkDrawingAttributes newAttributes)
        {
            if (newAttributes == null) return;

            this.InkCanvas?.InkPresenter.UpdateDefaultDrawingAttributes(newAttributes);
        }

        private async void DrawingArea_OnStrokesCollected(InkPresenter sender, InkStrokesCollectedEventArgs args)
        {
            await this.SaveDrawingAsync();

            this.OnInkChanged(args.Strokes, null);
        }

        private async void DrawingArea_OnStrokesErased(InkPresenter sender, InkStrokesErasedEventArgs args)
        {
            await this.SaveDrawingAsync();

            this.OnInkChanged(null, args.Strokes);
        }

        private void OnInkChanged(IEnumerable<InkStroke> strokesAdded, IEnumerable<InkStroke> strokesRemoved)
        {
            var strokeArgs = new InkChangedEventArgs(strokesAdded, strokesRemoved, !this.HasInk);

            this.InkChanged?.Invoke(this, strokeArgs);
            this.InkChangedCommand?.Execute(strokeArgs);
        }

        private async Task SaveDrawingAsync()
        {
            await this.fileSaveSemaphore.WaitAsync().ConfigureAwait(false);

            StorageFile tempFile;

            try
            {
                tempFile =
                    await
                    ApplicationData.Current.TemporaryFolder.CreateFileAsync(
                        $"{this.instanceIdentifier}.png",
                        CreationCollisionOption.ReplaceExisting);

                await this.InkCanvas.CaptureInkToFileAsync(this.renderBackground, tempFile, BitmapEncoder.PngEncoderId);
            }
            finally
            {
                this.fileSaveSemaphore.Release();
            }

            this.TemporaryDrawing = tempFile;

            var renderedArgs = new InkRenderedEventArgs(tempFile);

            this.InkRendered?.Invoke(this, renderedArgs);
            this.InkRenderedCommand?.Execute(renderedArgs);
        }

        /// <summary>
        /// Clears the drawing canvas.
        /// </summary>
        public async void Clear()
        {
            if (this.InkCanvas?.InkPresenter?.StrokeContainer != null)
            {
                var removedStrokes = this.InkCanvas.InkPresenter.StrokeContainer.GetStrokes().ToList();
                this.InkCanvas.InkPresenter.StrokeContainer.Clear();

                await this.SaveDrawingAsync();

                this.OnInkChanged(null, removedStrokes);
            }
        }
    }
}