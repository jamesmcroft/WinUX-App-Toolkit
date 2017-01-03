namespace WinUX.Xaml.Behaviors.ListViewBase
{
    using System.Linq;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for scrolling to an area of a ListViewBase control when triggered.
    /// </summary>
    public sealed class ScrollToSelectedItemBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Trigger"/>.
        /// </summary>
        public static readonly DependencyProperty TriggerProperty = DependencyProperty.Register(
            nameof(Trigger),
            typeof(bool),
            typeof(ScrollToSelectedItemBehavior),
            new PropertyMetadata(false));

        /// <summary>
        /// Defines the dependency property for <see cref="ScrollToMode"/>.
        /// </summary>
        public static readonly DependencyProperty ScrollToModeProperty =
            DependencyProperty.Register(
                nameof(ScrollToMode),
                typeof(ScrollToMode),
                typeof(ScrollToSelectedItemBehavior),
                new PropertyMetadata(
                    ScrollToMode.First,
                    (d, e) => ((ScrollToSelectedItemBehavior)d).OnTriggerChanged((bool)e.NewValue)));

        /// <summary>
        /// Defines the dependency propert for <see cref="Item"/>.
        /// </summary>
        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
            nameof(Item),
            typeof(object),
            typeof(ScrollToSelectedItemBehavior),
            new PropertyMetadata(null));

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

        /// <summary>
        /// Gets or sets the mode for scrolling to.
        /// </summary>
        public ScrollToMode ScrollToMode
        {
            get
            {
                return (ScrollToMode)this.GetValue(ScrollToModeProperty);
            }
            set
            {
                this.SetValue(ScrollToModeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the item to scroll to if the <see cref="ScrollToMode"/> is Item.
        /// </summary>
        public object Item
        {
            get
            {
                return this.GetValue(ItemProperty);
            }
            set
            {
                this.SetValue(ItemProperty, value);
            }
        }

        private ListViewBase ListViewBase => this.AssociatedObject as ListViewBase;

        private void OnTriggerChanged(bool triggered)
        {
            if (triggered && this.ListViewBase != null && this.ListViewBase.Items != null
                && this.ListViewBase.Items.Any())
            {
                object item = null;

                switch (this.ScrollToMode)
                {
                    case this.ScrollToMode.First:
                        item = this.ListViewBase.Items.FirstOrDefault();
                        break;
                    case this.ScrollToMode.Last:
                        item = this.ListViewBase.Items.LastOrDefault();
                        break;
                    case this.ScrollToMode.Item:
                        item = this.Item;
                        break;
                }

                if (item != null)
                {
                    this.ListViewBase.ScrollIntoView(item, ScrollIntoViewAlignment.Leading);
                }
            }
        }
    }
}