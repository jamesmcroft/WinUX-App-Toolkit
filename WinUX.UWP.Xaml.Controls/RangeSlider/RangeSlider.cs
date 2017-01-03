namespace WinUX.Xaml.Controls
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Shapes;

    using WinUX.Maths;

    /// <summary>
    /// Defines a control for providing a slider with two thumbs for ranges.
    /// </summary>
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "MinPressed", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "MaxPressed", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
    [TemplatePart(Name = "ContentContainer", Type = typeof(Border))]
    [TemplatePart(Name = "ActiveRectangle", Type = typeof(Rectangle))]
    [TemplatePart(Name = "MinThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "MaxThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "ContainerCanvas", Type = typeof(Canvas))]
    public partial class RangeSlider : Control
    {
        private const double Epsilon = 0.01;

        private Border contentContainer;

        private Rectangle activeRectangle;

        private Thumb minThumb;

        private Thumb maxThumb;

        private Canvas containerCanvas;

        private double oldSelectedValue;

        private bool areValuesAssigned;

        private bool isMinimumValueSet;

        private bool isMaximumValueSet;

        private bool isManipulatingMinThumb;

        private bool isManipulatingMaxThumb;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeSlider"/> class.
        /// </summary>
        public RangeSlider()
        {
            this.DefaultStyleKey = typeof(RangeSlider);
        }

        /// <summary>
        /// The value changed event.
        /// </summary>
        public event RangeSliderValueChangedEventHandler ValueChanged;

        /// <summary>
        /// Called when the template is applied to the control.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            if (this.contentContainer != null)
            {
                this.contentContainer.PointerPressed -= this.ContentContainer_OnPointerPressed;
                this.contentContainer.PointerMoved -= this.ContentContainer_OnPointerMoved;
                this.contentContainer.PointerReleased -= this.ContentContainer_OnPointerReleased;
                this.contentContainer.PointerExited -= this.ContentContainer_OnPointerExited;
            }

            if (this.minThumb != null)
            {
                this.minThumb.DragCompleted -= this.Thumb_OnDragCompleted;
                this.minThumb.DragDelta -= this.MinThumb_OnDragDelta;
                this.minThumb.DragStarted -= this.MinThumb_OnDragStarted;
            }

            if (this.maxThumb != null)
            {
                this.maxThumb.DragCompleted -= this.Thumb_OnDragCompleted;
                this.maxThumb.DragDelta -= this.MaxThumb_OnDragDelta;
                this.maxThumb.DragStarted -= this.MaxThumb_OnDragStarted;
            }

            if (this.containerCanvas != null)
            {
                this.containerCanvas.SizeChanged -= this.ContainerCanvas_OnSizeChanged;
            }

            this.IsEnabledChanged -= this.OnIsEnabledChanged;

            this.SetDefaultValues();
            this.areValuesAssigned = true;

            this.contentContainer = this.GetTemplateChild("ContentContainer") as Border;
            this.activeRectangle = this.GetTemplateChild("ActiveRectangle") as Rectangle;
            this.minThumb = this.GetTemplateChild("MinThumb") as Thumb;
            this.maxThumb = this.GetTemplateChild("MaxThumb") as Thumb;
            this.containerCanvas = this.GetTemplateChild("ContainerCanvas") as Canvas;

            if (this.contentContainer != null)
            {
                this.contentContainer.PointerPressed += this.ContentContainer_OnPointerPressed;
                this.contentContainer.PointerMoved += this.ContentContainer_OnPointerMoved;
                this.contentContainer.PointerReleased += this.ContentContainer_OnPointerReleased;
                this.contentContainer.PointerExited += this.ContentContainer_OnPointerExited;
            }

            if (this.minThumb != null)
            {
                this.minThumb.DragCompleted += this.Thumb_OnDragCompleted;
                this.minThumb.DragDelta += this.MinThumb_OnDragDelta;
                this.minThumb.DragStarted += this.MinThumb_OnDragStarted;
            }

            if (this.maxThumb != null)
            {
                this.maxThumb.DragCompleted += this.Thumb_OnDragCompleted;
                this.maxThumb.DragDelta += this.MaxThumb_OnDragDelta;
                this.maxThumb.DragStarted += this.MaxThumb_OnDragStarted;
            }

            if (this.containerCanvas != null)
            {
                this.containerCanvas.SizeChanged += this.ContainerCanvas_OnSizeChanged;
            }

            VisualStateManager.GoToState(this, this.IsEnabled ? "Normal" : "Disabled", false);

            this.IsEnabledChanged += this.OnIsEnabledChanged;

            base.OnApplyTemplate();
        }

        private void ContentContainer_OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            var position = e.GetCurrentPoint(this.contentContainer).Position.X;
            var normalizedPosition = ((position / this.contentContainer.ActualWidth) * (this.Maximum - this.Minimum))
                                     + this.Minimum;

            if (this.isManipulatingMinThumb)
            {
                this.isManipulatingMinThumb = false;
                this.containerCanvas.IsHitTestVisible = true;
                this.ValueChanged?.Invoke(
                    this,
                    new RangeSliderValueChangedEventArgs(
                        RangeSliderThumb.Minimum,
                        this.SelectedMinimum,
                        normalizedPosition));
            }
            else if (this.isManipulatingMaxThumb)
            {
                this.isManipulatingMaxThumb = false;
                this.containerCanvas.IsHitTestVisible = true;
                this.ValueChanged?.Invoke(
                    this,
                    new RangeSliderValueChangedEventArgs(
                        RangeSliderThumb.Maximum,
                        this.SelectedMaximum,
                        normalizedPosition));
            }
        }

        private void ContentContainer_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var position = e.GetCurrentPoint(this.contentContainer).Position.X;
            var normalizedPosition = ((position / this.contentContainer.ActualWidth) * (this.Maximum - this.Minimum))
                                     + this.Minimum;

            if (this.isManipulatingMinThumb)
            {
                this.isManipulatingMinThumb = false;
                this.containerCanvas.IsHitTestVisible = true;
                this.ValueChanged?.Invoke(
                    this,
                    new RangeSliderValueChangedEventArgs(
                        RangeSliderThumb.Minimum,
                        this.SelectedMinimum,
                        normalizedPosition));
            }
            else if (this.isManipulatingMaxThumb)
            {
                this.isManipulatingMaxThumb = false;
                this.containerCanvas.IsHitTestVisible = true;
                this.ValueChanged?.Invoke(
                    this,
                    new RangeSliderValueChangedEventArgs(
                        RangeSliderThumb.Maximum,
                        this.SelectedMaximum,
                        normalizedPosition));
            }
        }

        private void ContentContainer_OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var position = e.GetCurrentPoint(this.contentContainer).Position.X;
            var normalizedPosition = ((position / this.contentContainer.ActualWidth) * (this.Maximum - this.Minimum))
                                     + this.Minimum;

            if (this.isManipulatingMinThumb && normalizedPosition < this.SelectedMaximum)
            {
                this.SelectedMinimum = this.DragThumb(this.minThumb, 0, Canvas.GetLeft(this.maxThumb), position);
            }
            else if (this.isManipulatingMaxThumb && normalizedPosition > this.SelectedMinimum)
            {
                this.SelectedMaximum = this.DragThumb(
                    this.maxThumb,
                    Canvas.GetLeft(this.minThumb),
                    this.containerCanvas.ActualWidth,
                    position);
            }
        }

        private void ContentContainer_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var position = e.GetCurrentPoint(this.contentContainer).Position.X;
            var normalizedPosition = ((position / this.contentContainer.ActualWidth) * (this.Maximum - this.Minimum))
                                     + this.Minimum;
            if (normalizedPosition < this.SelectedMinimum)
            {
                this.isManipulatingMinThumb = true;
                this.containerCanvas.IsHitTestVisible = false;
            }
            else if (normalizedPosition > this.SelectedMaximum)
            {
                this.isManipulatingMaxThumb = true;
                this.containerCanvas.IsHitTestVisible = false;
            }
        }

        private void ContainerCanvas_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateThumbs();
        }

        private void SetDefaultValues()
        {
            if (this.Minimum > this.Maximum)
            {
                this.Minimum = this.Maximum;
            }

            if (MathHelper.AreClose(this.Minimum, this.Maximum))
            {
                this.Maximum += Epsilon;
            }

            if (!this.isMaximumValueSet)
            {
                this.SelectedMaximum = this.Maximum;
            }

            if (!this.isMinimumValueSet)
            {
                this.SelectedMinimum = this.Minimum;
            }

            if (this.SelectedMinimum < this.Minimum)
            {
                this.SelectedMinimum = this.Minimum;
            }

            if (this.SelectedMaximum < this.Minimum)
            {
                this.SelectedMaximum = this.Minimum;
            }

            if (this.SelectedMinimum > this.Maximum)
            {
                this.SelectedMinimum = this.Maximum;
            }

            if (this.SelectedMaximum > this.Maximum)
            {
                this.SelectedMaximum = this.Maximum;
            }

            if (this.SelectedMaximum < this.SelectedMinimum)
            {
                this.SelectedMinimum = this.SelectedMaximum;
            }
        }

        private void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            if (!this.areValuesAssigned)
            {
                return;
            }

            if (this.Maximum < newMinimum)
            {
                this.Maximum = newMinimum + Epsilon;
            }

            if (this.SelectedMinimum < newMinimum)
            {
                this.SelectedMinimum = newMinimum;
            }

            if (this.SelectedMaximum < newMinimum)
            {
                this.SelectedMaximum = newMinimum;
            }

            if (newMinimum < oldMinimum)
            {
                this.UpdateThumbs();
            }
        }

        private void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            if (!this.areValuesAssigned)
            {
                return;
            }

            if (this.Minimum > newMaximum)
            {
                this.Minimum = newMaximum - Epsilon;
            }

            if (this.SelectedMaximum > newMaximum)
            {
                this.SelectedMaximum = newMaximum;
            }

            if (this.SelectedMinimum > newMaximum)
            {
                this.SelectedMinimum = newMaximum;
            }

            if (newMaximum > oldMaximum)
            {
                this.UpdateThumbs();
            }
        }

        private void OnSelectedMinimumChanged(double newSelectedMinimum)
        {
            if (!this.areValuesAssigned)
            {
                return;
            }

            this.isMinimumValueSet = true;
            if (this.areValuesAssigned)
            {
                if (newSelectedMinimum < this.Minimum)
                {
                    this.SelectedMinimum = this.Minimum;
                    return;
                }

                if (newSelectedMinimum > this.Maximum)
                {
                    this.SelectedMinimum = this.Maximum;
                    return;
                }

                this.UpdateThumbs();

                if (newSelectedMinimum > this.SelectedMaximum)
                {
                    this.SelectedMaximum = newSelectedMinimum;
                }
            }
            else
            {
                this.UpdateThumbs();
            }
        }

        private void OnSelectedMaximumChanged(double newSelectedMaximum)
        {
            if (!this.areValuesAssigned)
            {
                return;
            }

            this.isMaximumValueSet = true;
            if (this.areValuesAssigned)
            {
                if (newSelectedMaximum < this.Minimum)
                {
                    this.SelectedMaximum = this.Minimum;
                    return;
                }

                if (newSelectedMaximum > this.Maximum)
                {
                    this.SelectedMaximum = this.Maximum;
                    return;
                }

                this.UpdateThumbs();

                if (newSelectedMaximum < this.SelectedMinimum)
                {
                    this.SelectedMinimum = newSelectedMaximum;
                }
            }
            else
            {
                this.UpdateThumbs();
            }
        }

        private void UpdateThumbs()
        {
            if (this.containerCanvas == null)
            {
                return;
            }

            var relativeLeft = ((this.SelectedMinimum - this.Minimum) / (this.Maximum - this.Minimum))
                               * this.containerCanvas.ActualWidth;
            var relativeRight = ((this.SelectedMaximum - this.Minimum) / (this.Maximum - this.Minimum))
                                * this.containerCanvas.ActualWidth;

            Canvas.SetLeft(this.minThumb, relativeLeft);
            Canvas.SetLeft(this.activeRectangle, relativeLeft);

            Canvas.SetLeft(this.maxThumb, relativeRight);

            this.activeRectangle.Width = Math.Max(0, Canvas.GetLeft(this.maxThumb) - Canvas.GetLeft(this.minThumb));
        }

        private void MinThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            this.SelectedMinimum = this.DragThumb(
                this.minThumb,
                0,
                Canvas.GetLeft(this.maxThumb),
                Canvas.GetLeft(this.minThumb) + e.HorizontalChange);
        }

        private void MaxThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            this.SelectedMaximum = this.DragThumb(
                this.maxThumb,
                Canvas.GetLeft(this.minThumb),
                this.containerCanvas.ActualWidth,
                Canvas.GetLeft(this.maxThumb) + e.HorizontalChange);
        }

        private double DragThumb(UIElement thumb, double min, double max, double nextPos)
        {
            nextPos = Math.Max(min, nextPos);
            nextPos = Math.Min(max, nextPos);

            Canvas.SetLeft(thumb, nextPos);

            return this.Minimum + ((nextPos / this.containerCanvas.ActualWidth) * (this.Maximum - this.Minimum));
        }

        private void MinThumb_OnDragStarted(object sender, DragStartedEventArgs e)
        {
            Canvas.SetZIndex(this.minThumb, 10);
            Canvas.SetZIndex(this.maxThumb, 0);
            this.oldSelectedValue = this.SelectedMinimum;

            VisualStateManager.GoToState(this, "MinPressed", true);
        }

        private void MaxThumb_OnDragStarted(object sender, DragStartedEventArgs e)
        {
            Canvas.SetZIndex(this.minThumb, 0);
            Canvas.SetZIndex(this.maxThumb, 10);
            this.oldSelectedValue = this.SelectedMaximum;
            VisualStateManager.GoToState(this, "MaxPressed", true);
        }

        private void Thumb_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.ValueChanged?.Invoke(
                this,
                sender.Equals(this.minThumb)
                    ? new RangeSliderValueChangedEventArgs(
                          RangeSliderThumb.Minimum,
                          this.oldSelectedValue,
                          this.SelectedMinimum)
                    : new RangeSliderValueChangedEventArgs(
                          RangeSliderThumb.Maximum,
                          this.oldSelectedValue,
                          this.SelectedMaximum));

            VisualStateManager.GoToState(this, "Normal", true);
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState(this, this.IsEnabled ? "Normal" : "Disabled", true);
        }
    }
}