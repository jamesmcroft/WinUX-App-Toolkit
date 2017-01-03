namespace WinUX.Xaml.Behaviors.MapControl
{
    using Windows.Foundation;
    using Windows.UI;
    using Windows.UI.Core;
    using Windows.UI.Input.Inking;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Maps;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for attaching an InkCanvas to a MapControl to support inking.
    /// </summary>
    public sealed class MapInkingBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="InkingPenShape"/>.
        /// </summary>
        public static readonly DependencyProperty InkingPenShapeProperty =
            DependencyProperty.Register(
                nameof(InkingPenShape),
                typeof(PenTipShape),
                typeof(MapInkingBehavior),
                new PropertyMetadata(PenTipShape.Rectangle, (d, e) => ((MapInkingBehavior)d).UpdateInkingAttributes()));

        /// <summary>
        /// Defines the dependency property for the <see cref="InkingPenSize"/>
        /// </summary>
        public static readonly DependencyProperty InkingPenSizeProperty =
            DependencyProperty.Register(
                nameof(InkingPenSize),
                typeof(Size),
                typeof(MapInkingBehavior),
                new PropertyMetadata(new Size(4, 4), (d, e) => ((MapInkingBehavior)d).UpdateInkingAttributes()));

        /// <summary>
        /// Defines the dependency property for the <see cref="IgnoreInkingPressure"/>.
        /// </summary>
        public static readonly DependencyProperty IgnoreInkingPressureProperty =
            DependencyProperty.Register(
                nameof(IgnoreInkingPressure),
                typeof(bool),
                typeof(MapInkingBehavior),
                new PropertyMetadata(false, (d, e) => ((MapInkingBehavior)d).UpdateInkingAttributes()));

        /// <summary>
        /// Defines the dependency property for the <see cref="InkingColor"/>.
        /// </summary>
        public static readonly DependencyProperty InkingColorProperty = DependencyProperty.Register(
            nameof(InkingColor),
            typeof(Color),
            typeof(MapInkingBehavior),
            new PropertyMetadata(Colors.Orange, (d, e) => ((MapInkingBehavior)d).UpdateInkingAttributes()));

        /// <summary>
        /// Gets or sets the color to use for the ink.
        /// </summary>
        public Color InkingColor
        {
            get
            {
                return (Color)this.GetValue(InkingColorProperty);
            }
            set
            {
                this.SetValue(InkingColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore the physical pen's pressure when inking.
        /// </summary>
        public bool IgnoreInkingPressure
        {
            get
            {
                return (bool)this.GetValue(IgnoreInkingPressureProperty);
            }
            set
            {
                this.SetValue(IgnoreInkingPressureProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the pen tip to use for the ink.
        /// </summary>
        public Size InkingPenSize
        {
            get
            {
                return (Size)this.GetValue(InkingPenSizeProperty);
            }
            set
            {
                this.SetValue(InkingPenSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the shape of the pen tip to use for the ink.
        /// </summary>
        public PenTipShape InkingPenShape
        {
            get
            {
                return (PenTipShape)this.GetValue(InkingPenShapeProperty);
            }
            set
            {
                this.SetValue(InkingPenShapeProperty, value);
            }
        }

        private MapControl MapControl => this.AssociatedObject as MapControl;

        private InkCanvas inkingCanvas;

        /// <summary>
        /// Called after the behavior is attached to the <see cref="Microsoft.Xaml.Interactivity.Behavior.AssociatedObject" />.
        /// </summary>
        protected override void OnAttached()
        {
            if (this.MapControl != null)
            {
                this.MapControl.SizeChanged += this.MapControlOnSizeChanged;

                this.inkingCanvas = new InkCanvas { IsHitTestVisible = false };

                Canvas.SetZIndex(this.inkingCanvas, 99999);

                this.InitializeInkingCanvas();

                this.MapControl.Children.Add(this.inkingCanvas);
            }
        }

        private void MapControlOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            this.UpdateInkingCanvasSize();
        }

        private void UpdateInkingCanvasSize()
        {
            if (this.inkingCanvas != null && this.MapControl != null)
            {
                this.inkingCanvas.Width = this.MapControl.ActualWidth;
                this.inkingCanvas.Height = this.MapControl.ActualHeight;
            }
        }

        private void InitializeInkingCanvas()
        {
            if (this.inkingCanvas != null)
            {
                this.inkingCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Pen;

                this.UpdateInkingAttributes();
                this.UpdateInkingCanvasSize();
            }
        }

        private void UpdateInkingAttributes()
        {
            var drawingAttributes = this.inkingCanvas.InkPresenter.CopyDefaultDrawingAttributes();

            drawingAttributes.PenTip = this.InkingPenShape;
            drawingAttributes.Size = this.InkingPenSize;
            drawingAttributes.IgnorePressure = this.IgnoreInkingPressure;
            drawingAttributes.Color = this.InkingColor;

            this.inkingCanvas.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);
        }
    }
}