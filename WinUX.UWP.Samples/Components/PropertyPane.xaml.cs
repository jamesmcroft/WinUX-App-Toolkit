namespace WinUX.UWP.Samples.Components
{
    using System;

    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    public sealed partial class PropertyPane
    {
        private bool isContextSet;

        public PropertyPane()
        {
            this.InitializeComponent();
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (this.isContextSet)
            {
                return;
            }

            this.isContextSet = true;

            var sample = this.DataContext as Sample;

            this.RootPanel.Children.Clear();

            if (sample != null)
            {
                var bindingSource = sample.BindingSource;

                if (bindingSource == null)
                {
                    return;
                }

                foreach (var property in bindingSource.Properties)
                {
                    this.AddPropertyLabel(property);

                    Control propertyControl;
                    DependencyProperty associatedProperty;

                    switch (property.Type)
                    {
                        case SamplePropertyType.Enum:
                            var enumComboBox = new ComboBox
                            {
                                ItemsSource = Enum.GetNames(property.DefaultValue.GetType()),
                                SelectedItem = property.DefaultValue.ToString()
                            };

                            propertyControl = enumComboBox;
                            associatedProperty = Selector.SelectedItemProperty;
                            break;
                        case SamplePropertyType.Boolean:
                            var toggleSwitch = new ToggleSwitch();

                            propertyControl = toggleSwitch;
                            associatedProperty = ToggleSwitch.IsOnProperty;
                            break;
                        case SamplePropertyType.Integer:
                            var slider = new Slider();
                            var options = property as IntegerSampleProperty;

                            if (options != null)
                            {
                                slider.Minimum = options.MinValue;
                                slider.Maximum = options.MaxValue;
                            }

                            propertyControl = slider;
                            associatedProperty = RangeBase.ValueProperty;
                            break;
                        default:
                            var textBox = new TextBox { Text = property.DefaultValue.ToString() };

                            propertyControl = textBox;
                            associatedProperty = TextBox.TextProperty;
                            break;
                    }

                    var dataBinding = new Binding
                                          {
                                              Source = bindingSource.Bindings,
                                              Path = new PropertyPath(property.Name + ".Value"),
                                              Mode = BindingMode.TwoWay
                                          };

                    propertyControl.SetBinding(associatedProperty, dataBinding);
                    propertyControl.Margin = new Thickness(0, 10, 0, 10);
                    this.RootPanel.Children.Add(propertyControl);
                }
            }
        }

        private void AddPropertyLabel(SampleProperty property)
        {
            var propertyLabel = new TextBlock { Text = property.Name, Foreground = new SolidColorBrush(Colors.Black) };
            this.RootPanel.Children.Add(propertyLabel);
        }
    }
}