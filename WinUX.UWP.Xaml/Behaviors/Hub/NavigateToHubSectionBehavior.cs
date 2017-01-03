namespace WinUX.Xaml.Behaviors.Hub
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior to scroll to a specific HubSection within the associated Hub when triggered.
    /// </summary>
    public sealed class NavigateToHubSectionBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Section"/>.
        /// </summary>
        public static readonly DependencyProperty SectionProperty = DependencyProperty.Register(
            nameof(Section),
            typeof(HubSection),
            typeof(NavigateToHubSectionBehavior),
            new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for <see cref="Trigger"/>.
        /// </summary>
        public static readonly DependencyProperty TriggerProperty = DependencyProperty.Register(
            nameof(Trigger),
            typeof(bool),
            typeof(NavigateToHubSectionBehavior),
            new PropertyMetadata(false, (d, e) => ((NavigateToHubSectionBehavior)d).OnTriggerChanged((bool)e.NewValue)));

        /// <summary>
        /// Gets or sets the <see cref="HubSection"/> to scroll to.
        /// </summary>
        public HubSection Section
        {
            get
            {
                return (HubSection)this.GetValue(SectionProperty);
            }
            set
            {
                this.SetValue(SectionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the trigger.
        /// </summary>
        public bool Trigger
        {
            get
            {
                return (bool)this.GetValue(TriggerProperty);
            }
            set
            {
                this.SetValue(TriggerProperty, value);
            }
        }

        private Hub Hub => this.AssociatedObject as Hub;

        private void OnTriggerChanged(bool triggered)
        {
            if (!triggered) return;

            if (this.Hub != null && this.Section != null)
            {
                this.Hub.ScrollToSection(this.Section);
            }
        }
    }
}