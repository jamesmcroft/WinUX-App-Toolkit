namespace WinUX.UWP.Samples.Components
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public sealed class SamplePropertyValue : INotifyPropertyChanged
    {
        private object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePropertyValue"/> class.
        /// </summary>
        /// <param name="val">
        /// The value of the property.
        /// </param>
        public SamplePropertyValue(object val)
        {
            this.value = val;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value == value) return;

                this.value = value;
                this.OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}