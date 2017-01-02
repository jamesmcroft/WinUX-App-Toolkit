namespace WinUX.UWP.Mvvm.Input
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    /// Defines a command for executing an action with a typed parameter.
    /// </summary>
    /// <typeparam name="T">
    /// The type of parameter.
    /// </typeparam>
    public sealed class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> executeAction;

        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="executeAction">
        /// The action to execute when called.
        /// </param>
        /// <exception cref="DelegateCommand">
        /// Thrown if the execute action is null.
        /// </exception>
        public DelegateCommand(Action<T> executeAction)
            : this(executeAction, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="executeAction">
        /// The action to execute when called.
        /// </param>
        /// <param name="canExecute">
        /// The function to call to determine if the command can execute the action.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the execute action is null.
        /// </exception>
        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecute)
        {
            if (executeAction == null) throw new ArgumentNullException(nameof(executeAction));

            this.executeAction = executeAction;
            this.canExecute = canExecute ?? (e => true);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public bool CanExecute(object parameter)
        {
            try
            {
                return this.canExecute(ConvertParameterValue(parameter));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (!this.CanExecute(parameter))
            {
                return;
            }

            try
            {
                this.executeAction(ConvertParameterValue(parameter));
            }
            catch
            {
                Debugger.Break();
            }
        }

        /// <summary>
        /// Raises the can execute changed event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private static T ConvertParameterValue(object parameter)
        {
            parameter = parameter is T ? parameter : Convert.ChangeType(parameter, typeof(T));
            return (T)parameter;
        }
    }
}