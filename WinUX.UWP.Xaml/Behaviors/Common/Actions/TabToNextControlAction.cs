namespace WinUX.Xaml.Behaviors.Common.Actions
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines an action which will attempt to set focus on the next sibling of the associated control.
    /// </summary>
    public sealed class TabToNextControlAction : IAction
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="sender">
        /// The <see cref="T:System.Object" /> that is passed to the action by the behavior. Generally this is <seealso cref="P:Microsoft.Xaml.Interactivity.IBehavior.AssociatedObject" /> or a target object.
        /// </param>
        /// <param name="parameter">
        /// The value of this parameter is determined by the caller.
        /// </param>
        /// <remarks>
        /// An example of parameter usage is EventTriggerBehavior, which passes the EventArgs as a parameter to its actions.
        /// </remarks>
        /// <returns>
        /// Returns the result of the action.
        /// </returns>
        public object Execute(object sender, object parameter)
        {
            var control = sender as FrameworkElement;
            if (control == null) return false;

            var nextControl = control.FindNextSiblingOfType<Control>();
            nextControl?.Focus(FocusState.Keyboard);
            return true;
        }
    }
}