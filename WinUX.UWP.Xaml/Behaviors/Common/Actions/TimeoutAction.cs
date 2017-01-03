namespace WinUX.Xaml.Behaviors.Common.Actions
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Markup;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// An action for a behavior to execute a set of actions on a timeout.
    /// </summary>
    [ContentProperty(Name = nameof(Actions))]
    public sealed class TimeoutAction : DependencyObject, IAction
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Actions"/>.
        /// </summary>
        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(
            nameof(Actions),
            typeof(ActionCollection),
            typeof(TimeoutAction),
            new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for <see cref="Milliseconds"/>.
        /// </summary>
        public static readonly DependencyProperty MillisecondsProperty =
            DependencyProperty.Register(
                nameof(Milliseconds),
                typeof(int),
                typeof(TimeoutAction),
                new PropertyMetadata(5000));

        /// <summary>
        /// Gets the collection of actions to perform when this action times out.
        /// </summary>
        public ActionCollection Actions
        {
            get
            {
                var actions = (ActionCollection)this.GetValue(ActionsProperty);
                if (actions != null)
                {
                    return actions;
                }

                actions = new ActionCollection();
                this.SetValue(ActionsProperty, actions);
                return actions;
            }
        }

        /// <summary>
        /// Gets or sets the timeout value in milliseconds.
        /// </summary>
        public int Milliseconds
        {
            get
            {
                return (int)this.GetValue(MillisecondsProperty);
            }
            set
            {
                this.SetValue(MillisecondsProperty, value);
            }
        }

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
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(this.Milliseconds) };
            timer.Tick += this.OnTimerTick;
            timer.Start();

            return null;
        }

        private void OnTimerTick(object sender, object e)
        {
            var timer = sender as DispatcherTimer;
            if (timer != null)
            {
                timer.Tick -= this.OnTimerTick;
                timer.Stop();
            }

            Interaction.ExecuteActions(this, this.Actions, null);
        }
    }
}