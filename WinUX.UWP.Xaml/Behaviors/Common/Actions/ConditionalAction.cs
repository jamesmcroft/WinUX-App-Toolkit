namespace WinUX.Xaml.Behaviors.Common.Actions
{
    using System;
    using System.Collections.Generic;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Markup;

    using Microsoft.Xaml.Interactivity;

    using WinUX.Data;

    /// <summary>
    /// Defines an action which is executed depending on the conditions.
    /// </summary>
    [ContentProperty(Name = nameof(Actions))]
    public sealed class ConditionalAction : DependencyObject, IAction
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Operator"/>.
        /// </summary>
        public static readonly DependencyProperty OperatorProperty = DependencyProperty.Register(
            nameof(Operator),
            typeof(ConditionalOperator),
            typeof(ConditionalAction),
            new PropertyMetadata(ConditionalOperator.EqualToRight));

        /// <summary>
        /// Defines the dependency propery for <see cref="LeftValue"/>.
        /// </summary>
        public static readonly DependencyProperty LeftValueProperty = DependencyProperty.Register(
            nameof(LeftValue),
            typeof(object),
            typeof(ConditionalAction),
            new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for <see cref="RightValue"/>.
        /// </summary>
        public static readonly DependencyProperty RightValueProperty = DependencyProperty.Register(
            nameof(RightValue),
            typeof(object),
            typeof(ConditionalAction),
            new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for <see cref="Actions"/>.
        /// </summary>
        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(
            nameof(Actions),
            typeof(ActionCollection),
            typeof(TimeoutAction),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the conditional operator between the two values.
        /// </summary>
        public ConditionalOperator Operator
        {
            get
            {
                return (ConditionalOperator)this.GetValue(OperatorProperty);
            }
            set
            {
                this.SetValue(OperatorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the left hand value.
        /// </summary>
        public object LeftValue
        {
            get
            {
                return this.GetValue(LeftValueProperty);
            }
            set
            {
                this.SetValue(LeftValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the right hand value.
        /// </summary>
        public object RightValue
        {
            get
            {
                return this.GetValue(RightValueProperty);
            }
            set
            {
                this.SetValue(RightValueProperty, value);
            }
        }

        /// <summary>
        /// Gets the collection of actions to perform when the conditions are met.
        /// </summary>
        public ActionCollection Actions
        {
            get
            {
                var actions = this.GetValue(ActionsProperty) as ActionCollection;
                if (actions != null)
                {
                    return actions;
                }

                actions = new ActionCollection();
                this.SetValue(ActionsProperty, actions);
                return actions;
            }
        }

        private static int Compare<T>(T left, T right)
        {
            return Comparer<T>.Default.Compare(left, right);
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
            var leftType = this.LeftValue?.GetType();
            var rightValue = this.RightValue == null ? null : Convert.ChangeType(this.RightValue, leftType);

            switch (this.Operator)
            {
                default:
                    if (Compare(this.LeftValue, rightValue) == 0)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.NotEqualToRight:
                    if (Compare(this.LeftValue, rightValue) != 0)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.LessThanRight:
                    if (Compare(this.LeftValue, rightValue) > 0)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.LessThanOrEqualToRight:
                    if (Compare(this.LeftValue, rightValue) >= 0)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.GreaterThanRight:
                    if (Compare(this.LeftValue, rightValue) < 0)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.GreaterThanOrEqualToRight:
                    if (Compare(this.LeftValue, rightValue) <= 0)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.IsNull:
                    if (this.LeftValue == null)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.IsNotNull:
                    if (this.LeftValue != null)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.IsTrue:
                    if ((bool?)this.LeftValue ?? false)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.IsFalse:
                    if (!(bool?)this.LeftValue ?? false)
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.IsNullOrEmpty:
                    if (string.IsNullOrEmpty(this.LeftValue as string))
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
                case ConditionalOperator.IsNotNullOrEmpty:
                    if (!string.IsNullOrEmpty(this.LeftValue as string))
                    {
                        Interaction.ExecuteActions(this, this.Actions, parameter);
                    }
                    break;
            }

            return null;
        }
    }
}