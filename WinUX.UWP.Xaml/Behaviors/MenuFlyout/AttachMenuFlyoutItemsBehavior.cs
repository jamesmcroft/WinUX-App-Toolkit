namespace WinUX.Xaml.Behaviors.MenuFlyout
{
    using System.Collections.Generic;
    using System.Linq;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior to attach a <see cref="MenuFlyoutItem"/> collection to a <see cref="MenuFlyout"/> control.
    /// </summary>
    public sealed class AttachMenuFlyoutItemsBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="ItemsSource"/>.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(object),
            typeof(AttachMenuFlyoutItemsBehavior),
            new PropertyMetadata(null, (d, e) => ((AttachMenuFlyoutItemsBehavior)d).UpdateMenuFlyoutItems(e.NewValue)));

        /// <summary>
        /// Gets or sets the collection of <see cref="MenuFlyoutItem"/> to attach.
        /// </summary>
        public object ItemsSource
        {
            get
            {
                return this.GetValue(ItemsSourceProperty);
            }
            set
            {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        private MenuFlyout MenuFlyout => this.AssociatedObject as MenuFlyout;

        /// <summary>
        /// Called after the behavior is attached to the <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject" />.
        /// </summary>
        protected override void OnAttached()
        {
            if (this.MenuFlyout != null)
            {
                this.UpdateMenuFlyoutItems(this.ItemsSource);
            }
        }

        private void UpdateMenuFlyoutItems(object itemsSourceObject)
        {
            if (this.MenuFlyout?.Items != null)
            {
                var menuFlyoutItemBases = this.MenuFlyout.Items.ToList();
                if (menuFlyoutItemBases != null)
                {
                    foreach (var item in menuFlyoutItemBases)
                    {
                        this.MenuFlyout.Items.Remove(item);
                    }
                }

                var itemsSource = itemsSourceObject as IEnumerable<MenuFlyoutItem>;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        this.MenuFlyout.Items.Add(item);
                    }
                }
            }
        }
    }
}