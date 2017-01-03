namespace WinUX.UWP.Samples.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using WinUX.Common;
    using WinUX.Mvvm.Services;
    using WinUX.Storage;

    public sealed class Sample
    {
        /// <summary>
        /// Gets or sets the name of the sample.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the sample.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the source page type for the sample.
        /// </summary>
        public string SourcePageType { get; set; }

        /// <summary>
        /// Gets or sets the string URI to the source file used for understanding bindings.
        /// </summary>
        public string BindingFileSource { get; set; }

        /// <summary>
        /// Gets or sets the name of the group containing this sample.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets the source used for binding the properties of the sample.
        /// </summary>
        public SampleProperties BindingSource { get; private set; }

        /// <summary>
        /// Initializes the sample.
        /// </summary>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public async Task InitializeAsync()
        {
            if (string.IsNullOrWhiteSpace(this.BindingFileSource)) return;

            if (this.BindingSource == null)
            {
                using (var stream = await StorageHelper.GetPackagedFileAsStreamAsync($"Samples/{this.GroupName}/{this.Name}/{this.BindingFileSource}"))
                {
                    var bindingSource = await stream.ReadAsStringAsync();

                    var regex = new Regex(@"@\[(?<name>.+?):(?<type>.+?):(?<value>.+?)(:(?<parameters>.*))*\]");

                    this.BindingSource = new SampleProperties();
                    var bindings = (IDictionary<string, object>)this.BindingSource.Bindings;

                    foreach (Match match in regex.Matches(bindingSource))
                    {
                        var propertyName = match.Groups["name"].Value;
                        var propertyTypeString = match.Groups["type"].Value;
                        var defaultValueString = match.Groups["value"].Value;

                        var propertyType = ParseHelper.SafeParseEnum<SamplePropertyType>(propertyTypeString);

                        SampleProperty property;

                        switch (propertyType)
                        {
                            case SamplePropertyType.Enum:
                                try
                                {
                                    property = new SampleProperty();
                                    var split = defaultValueString.Split('.');
                                    var typeString = string.Join(".", split.Take(split.Length - 1));
                                    var enumType = typeString.GetTypeByName(true, true);
                                    property.DefaultValue = Enum.Parse(enumType, split.Last());
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                                break;
                            case SamplePropertyType.Boolean:
                                property = new SampleProperty
                                               {
                                                   DefaultValue = ParseHelper.SafeParseBool(defaultValueString)
                                               };
                                break;
                            case SamplePropertyType.Integer:
                                try
                                {
                                    var integerProperty = new IntegerSampleProperty
                                                              {
                                                                  DefaultValue =
                                                                      ParseHelper.SafeParseDouble(defaultValueString)
                                                              };

                                    var parameters = match.Groups["parameters"].Value;
                                    var split = parameters.Split('-');

                                    integerProperty.MinValue = ParseHelper.SafeParseDouble(split[0]);
                                    integerProperty.MaxValue = ParseHelper.SafeParseDouble(split[1]);

                                    property = integerProperty;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                                break;
                            default:
                                property = new SampleProperty() { DefaultValue = defaultValueString };
                                break;
                        }

                        property.Name = propertyName;
                        property.Code = match.Value;
                        property.Type = propertyType;
                        bindings[propertyName] = new SamplePropertyValue(property.DefaultValue);

                        this.BindingSource.Properties.Add(property);
                    }
                }
            }
        }

        /// <summary>
        /// Navigates to the sample.
        /// </summary>
        public void NavigateTo()
        {
            NavigationService.Current.Navigate(this.SourcePageType, this);
        }
    }
}