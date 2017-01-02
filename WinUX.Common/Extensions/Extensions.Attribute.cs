namespace WinUX
{
    using System.Linq;
    using System.Reflection;

    using WinUX.Attributes;

    /// <summary>
    /// Defines a collection of extensions for attributes.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the description attribute from the specified object.
        /// </summary>
        /// <param name="obj">
        /// The object to find a description attribute for.
        /// </param>
        /// <returns>
        /// Returns the description attribute value.
        /// </returns>
        public static string GetDescriptionAttribute(this object obj)
        {
            var objType = obj.GetType();
            var memberInfos = objType.GetTypeInfo().DeclaredMembers;

            var memberInfo = memberInfos.FirstOrDefault(x => x.Name == obj.ToString());
            var attribute =
                memberInfo?.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(DescriptionAttribute));

            return attribute != null ? attribute.ConstructorArguments[0].Value.ToString() : obj.ToString();
        }
    }
}