namespace WinUX.Xaml.VisualStateTriggers
{
    using System;

    using Windows.UI.Xaml;

    /// <summary>
    /// Defines a base class for creating custom visual state triggers.
    /// </summary>
    public abstract class VisualStateTriggerBase : StateTriggerBase
    {
        private bool isActive;

        /// <summary>
        /// Event for when the active state of the trigger changes.
        /// </summary>
        public event EventHandler<bool> IsActiveChanged;

        /// <summary>
        /// Gets a value indicating whether the trigger is currently active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                if (this.isActive == value)
                {
                    return;
                }

                this.isActive = value;
                this.SetActive(value);

                this.IsActiveChanged?.Invoke(this, value);
            }
        }
    }
}