namespace WinUX
{
    using System.Linq;

    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a collection of extensions for the <see cref="ListViewBase"/> control.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Deselects all the items within the specified <see cref="ListViewBase"/> control.
        /// </summary>
        /// <param name="control">
        /// The control to deselect items from.
        /// </param>
        public static void DeselectAll(this ListViewBase control)
        {
            control.SelectedItems.Clear();
            control.SelectedItem = null;
        }

        /// <summary>
        /// Sets the selected item within the specified <see cref="ListViewBase"/> control.
        /// </summary>
        /// <param name="control">
        /// The control to select the item on.
        /// </param>
        /// <param name="item">
        /// The item to select.
        /// </param>
        public static void SetSelectedItem(this ListViewBase control, object item)
        {
            var controlItem = control.Items.FirstOrDefault(i => i.Equals(item));
            if (controlItem != null)
            {
                control.SelectedItem = controlItem;
            }
        }
    }
}