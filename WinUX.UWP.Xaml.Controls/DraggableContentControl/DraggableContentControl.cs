namespace WinUX.Xaml.Controls
{
    using Windows.Devices.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Defines a wrapper control for allowing content to be dragged around a UI.
    /// </summary>
    [TemplatePart(Name = "ManipulationGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "ContentPart", Type = typeof(ContentPresenter))]
    public sealed partial class DraggableContentControl : ContentControl
    {
        private readonly CompositeTransform compositeTransform = new CompositeTransform();

        private Pointer initialPointer;

        private Pointer secondaryPointer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DraggableContentControl"/> class.
        /// </summary>
        public DraggableContentControl()
        {
            this.DefaultStyleKey = typeof(DraggableContentControl);
        }

        /// <summary>
        /// Called when applying the control's template.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.ManipulationGrid != null)
            {
                this.ManipulationGrid.PointerPressed -= this.OnManipulationGridPointerPressed;
                this.ManipulationGrid.PointerReleased -= this.OnManipulationGridPointerReleased;
                this.ManipulationGrid.ManipulationDelta -= this.OnManipulationGridManipulationDelta;
                this.ManipulationGrid.PointerCaptureLost -= this.OnManipulationGridPointerReleased;
                this.ManipulationGrid.PointerExited -= this.OnManipulationGridPointerReleased;
            }

            this.ManipulationGrid = this.GetTemplateChild("ManipulationGrid") as Grid;
            this.ContentPart = this.GetTemplateChild("ContentPart") as ContentPresenter;

            if (this.ManipulationGrid != null)
            {
                this.ManipulationGrid.RenderTransform = this.compositeTransform;

                this.ManipulationGrid.PointerExited += this.OnManipulationGridPointerReleased;
                this.ManipulationGrid.PointerCaptureLost += this.OnManipulationGridPointerReleased;
                this.ManipulationGrid.PointerPressed += this.OnManipulationGridPointerPressed;
                this.ManipulationGrid.PointerReleased += this.OnManipulationGridPointerReleased;
                this.ManipulationGrid.ManipulationDelta += this.OnManipulationGridManipulationDelta;
            }
        }

        private void OnManipulationGridManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (this.initialPointer != null && this.secondaryPointer != null)
            {
                if (e.IsInertial)
                {
                    e.Complete();
                }

                this.UpdateCompositeTransform(e);

                e.Handled = true;
            }
        }

        private void UpdateCompositeTransform(ManipulationDeltaRoutedEventArgs e)
        {
            if (this.IsRotatingEnabled)
            {
                this.compositeTransform.Rotation += e.Delta.Rotation;
            }

            if (this.IsScalingEnabled)
            {
                this.compositeTransform.ScaleX *= e.Delta.Scale;
                this.compositeTransform.ScaleY *= e.Delta.Scale;
            }

            this.compositeTransform.TranslateX += e.Delta.Translation.X;
            this.compositeTransform.TranslateY += e.Delta.Translation.Y;
        }

        private void OnManipulationGridPointerReleased(object sender, PointerRoutedEventArgs args)
        {
            if (this.secondaryPointer != null && args.Pointer.PointerId == this.secondaryPointer.PointerId)
            {
                this.secondaryPointer = null;
            }

            if (this.initialPointer != null && args.Pointer.PointerId == this.initialPointer.PointerId)
            {
                this.initialPointer = this.secondaryPointer;
            }
        }

        private void OnManipulationGridPointerPressed(object sender, PointerRoutedEventArgs args)
        {
            if (args.Pointer.PointerDeviceType == PointerDeviceType.Touch)
            {
                if (this.initialPointer == null)
                {
                    this.initialPointer = args.Pointer;
                }
                else if (this.secondaryPointer == null)
                {
                    this.secondaryPointer = args.Pointer;
                }
            }
        }
    }
}