namespace WinUX.UWP.Controls.ToggleButton
{
    using System.Linq;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;

    using WinUX.UWP.Extensions;

    /// <summary>
    /// Defines an extension to the <see cref="ToggleButton"/> control to add grouping support similar to <see cref="RadioButton"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Defines the dependency property for the group's parent.
        /// </summary>
        public static readonly DependencyProperty GroupParentProperty =
            DependencyProperty.RegisterAttached(
                "GroupParent",
                typeof(object),
                typeof(Extensions),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the group's name.
        /// </summary>
        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.RegisterAttached(
            "GroupName",
            typeof(string),
            typeof(Extensions),
            new PropertyMetadata(null, OnGroupNameChanged));

        /// <summary>
        /// Gets the group name for the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/>.
        /// </param>
        /// <returns>
        /// Returns the group name as a <see cref="string"/>.
        /// </returns>
        public static string GetGroupName(DependencyObject obj)
        {
            return obj.GetValue(GroupNameProperty) as string;
        }

        /// <summary>
        /// Sets the group name for the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/>.
        /// </param>
        /// <param name="value">
        /// The group name to set.
        /// </param>
        public static void SetGroupName(DependencyObject obj, string value)
        {
            obj.SetValue(GroupNameProperty, value);
        }

        /// <summary>
        /// Gets the group parent object for the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/>.
        /// </param>
        /// <returns>
        /// Returns the group parent as an object.
        /// </returns>
        public static object GetGroupParent(DependencyObject obj)
        {
            return obj.GetValue(GroupParentProperty);
        }

        /// <summary>
        /// Sets the group parent object for the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/>.
        /// </param>
        /// <param name="value">
        /// The group parent to set.
        /// </param>
        public static void SetGroupParent(DependencyObject obj, object value)
        {
            obj.SetValue(GroupParentProperty, value);
        }


        private static void OnGroupNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var control = d as ToggleButton;
            if (control != null)
            {
                if (args.OldValue == null && args.NewValue != null)
                {
                    control.Checked += OnToggleButtonChecked;
                    control.Unchecked += OnToggleButtonUnchecked;
                }
                else if (args.NewValue == null)
                {
                    control.Checked -= OnToggleButtonChecked;
                    control.Unchecked -= OnToggleButtonUnchecked;
                }
            }
        }

        private static void OnToggleButtonUnchecked(object sender, RoutedEventArgs args)
        {
            var toggleButton = sender as ToggleButton;
            if (toggleButton != null)
            {
                var groupName = GetGroupName(toggleButton);
                var groupParent = GetGroupParent(toggleButton);

                var parentElement = groupParent == null ? toggleButton.FindAscendant<CommandBar>() : groupParent as UIElement;

                UpdateUncheckedToggleState(parentElement, groupName, toggleButton);
            }

        }

        private static void OnToggleButtonChecked(object sender, RoutedEventArgs args)
        {
            var toggleButton = sender as ToggleButton;
            if (toggleButton != null)
            {
                var groupName = GetGroupName(toggleButton);
                var groupParent = GetGroupParent(toggleButton);

                var parentElement = groupParent == null ? toggleButton.FindAscendant<CommandBar>() : groupParent as UIElement;

                UpdateToggleState(parentElement, groupName, toggleButton);
            }
        }

        private static void UpdateUncheckedToggleState(
            DependencyObject parentElement,
            string groupName,
            ToggleButton toggleButton)
        {
            var childToggleButtons =
                parentElement?.GetDescendantsOfType<ToggleButton>().Where(x => x != null && x != toggleButton).ToList();

            if (childToggleButtons?.Count > 0
                && childToggleButtons.Where(x => GetGroupName(x) == groupName)
                    .All(x => x.IsChecked != null && !x.IsChecked.Value))
            {
                toggleButton.IsChecked = true;
            }
        }

        private static void UpdateToggleState(DependencyObject parentElement, string groupName, ToggleButton toggleButton)
        {
            var childToggleButtons =
                parentElement?.GetDescendantsOfType<ToggleButton>().Where(x => x != null && x != toggleButton).ToList();

            if (!(childToggleButtons?.Count > 0) || toggleButton.IsChecked == null || !toggleButton.IsChecked.Value)
            {
                return;
            }

            foreach (var tb in from tb in childToggleButtons
                               let toggleGroupName = GetGroupName(tb)
                               where toggleGroupName != null
                               where toggleGroupName == groupName
                               select tb)
            {
                tb.IsChecked = false;
            }
        }
    }
}