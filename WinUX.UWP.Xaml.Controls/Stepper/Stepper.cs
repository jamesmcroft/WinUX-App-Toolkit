namespace WinUX.Xaml.Controls
{
    using System;

    using Windows.UI.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    /// <summary>
    /// Defines a control that provides a user interface for incrementing or decrementing a value.
    /// </summary>
    [TemplatePart(Name = "AddButton", Type = typeof(Button))]
    [TemplatePart(Name = "SubtractButton", Type = typeof(Button))]
    [TemplatePart(Name = "ValueTextBlock", Type = typeof(TextBlock))]
    public partial class Stepper : Control
    {
        private readonly DispatcherTimer holdingTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stepper"/> class.
        /// </summary>
        public Stepper()
        {
            this.DefaultStyleKey = typeof(Stepper);

            this.holdingTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            this.holdingTimer.Tick += this.HoldingTimer_Tick;
        }

        /// <summary>
        /// Called when the template is applied to the control.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.AddButton != null)
            {
                this.AddButton.Click -= this.AddButton_OnClick;
                this.AddButton.Holding -= this.AddButton_Holding;
            }

            if (this.SubtractButton != null)
            {
                this.SubtractButton.Click -= this.SubtractButton_OnClick;
                this.SubtractButton.Holding -= this.SubtractButton_Holding;
            }

            this.AddButton = this.GetTemplateChild("AddButton") as Button;
            this.SubtractButton = this.GetTemplateChild("SubtractButton") as Button;
            this.ValueTextBlock = this.GetTemplateChild("ValueTextBlock") as TextBlock;

            if (this.AddButton != null)
            {
                this.AddButton.Click += this.AddButton_OnClick;
                this.AddButton.Holding += this.AddButton_Holding;
            }

            if (this.SubtractButton != null)
            {
                this.SubtractButton.Click += this.SubtractButton_OnClick;
                this.SubtractButton.Holding += this.SubtractButton_Holding;
            }

            this.UpdateForAutorepeat(this.Autorepeat);
        }

        private bool isHoldingSubtract;

        private bool isHoldingAdd;

        private void AddButton_Holding(object sender, HoldingRoutedEventArgs e)
        {
            this.isHoldingAdd = this.Autorepeat && e.HoldingState == HoldingState.Started;
        }

        private void SubtractButton_Holding(object sender, HoldingRoutedEventArgs e)
        {
            this.isHoldingSubtract = this.Autorepeat && e.HoldingState == HoldingState.Started;
        }

        private void HoldingTimer_Tick(object sender, object o)
        {
            if (this.Autorepeat)
            {
                if (this.isHoldingAdd)
                {
                    this.IncrementValue();
                }
                else if (this.isHoldingSubtract)
                {
                    this.DecrementValue();
                }
            }
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.IncrementValue();
        }

        private void SubtractButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DecrementValue();
        }

        private void Update()
        {
            var nextAdd = this.Value + this.StepValue;
            var nextSubtract = this.Value - this.StepValue;

            if (this.Value < this.MinimumValue)
            {
                if (!this.Wraps)
                {
                    this.Value = this.MinimumValue;
                }
                else
                {
                    var difference = this.MinimumValue - nextSubtract;
                    this.Value = this.MaximumValue - difference;
                }
            }
            else if (this.Value > this.MaximumValue)
            {
                if (!this.Wraps)
                {
                    this.Value = this.MaximumValue;
                }
                else
                {
                    var difference = nextAdd - this.MaximumValue;
                    this.Value = this.MinimumValue + difference;
                }
            }

            if (this.SubtractButton != null)
            {
                if (!this.Wraps)
                {
                    this.SubtractButton.IsEnabled = nextSubtract >= this.MinimumValue;
                }
                else
                {
                    this.SubtractButton.IsEnabled = true;
                }
            }

            if (this.AddButton != null)
            {
                if (!this.Wraps)
                {
                    this.AddButton.IsEnabled = nextAdd <= this.MaximumValue;
                }
                else
                {
                    this.AddButton.IsEnabled = true;
                }
            }

            if (this.ValueTextBlock != null)
            {
                this.ValueTextBlock.Text = string.IsNullOrWhiteSpace(this.ValueFormat)
                                               ? this.Value.ToString()
                                               : this.Value.ToString(this.ValueFormat);
            }
        }

        private void IncrementValue()
        {
            var newValue = this.Value + this.StepValue;

            if (newValue > this.MaximumValue)
            {
                if (this.Wraps)
                {
                    var difference = newValue - this.MaximumValue;
                    newValue = this.MinimumValue + difference;
                }
                else
                {
                    newValue = this.Value;
                }
            }

            this.Value = newValue;
            this.Update();
        }

        private void DecrementValue()
        {
            var newValue = this.Value - this.StepValue;

            if (newValue < this.MinimumValue)
            {
                if (this.Wraps)
                {
                    var difference = this.MinimumValue - newValue;
                    newValue = this.MaximumValue - difference;
                }
                else
                {
                    newValue = this.Value;
                }
            }

            this.Value = newValue;
            this.Update();
        }

        private void UpdateForAutorepeat(bool autorepeat)
        {
            if (!autorepeat)
            {
                this.isHoldingAdd = false;
                this.isHoldingSubtract = false;
                this.holdingTimer.Stop();
            }
            else
            {
                this.holdingTimer.Start();
            }

            this.Update();
        }
    }
}