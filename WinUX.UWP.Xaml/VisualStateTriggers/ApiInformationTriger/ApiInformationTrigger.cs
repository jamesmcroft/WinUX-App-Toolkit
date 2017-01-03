namespace WinUX.Xaml.VisualStateTriggers.ApiInformationTriger
{
    using Windows.Foundation.Metadata;
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines a visual state trigger that checks whether a specified API type exists.
    /// </summary>
    public sealed class ApiInformationTrigger : VisualStateTriggerBase
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Type"/>.
        /// </summary>
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            nameof(Type),
            typeof(string),
            typeof(ApiInformationTrigger),
            new PropertyMetadata(
                string.Empty,
                (d, e) => ((ApiInformationTrigger)d).OnTypeChanged(e.NewValue.ToString())));

        /// <summary>
        /// Gets or sets the API type to trigger on.
        /// </summary>
        public string Type
        {
            get
            {
                return (string)this.GetValue(TypeProperty);
            }
            set
            {
                this.SetValue(TypeProperty, value);
            }
        }

        private void OnTypeChanged(string newType)
        {
            this.IsActive = !string.IsNullOrWhiteSpace(newType) && ApiInformation.IsTypePresent(newType);
        }
    }
}