namespace WinUX
{
    using Windows.System;
    using Windows.UI.Core;

    /// <summary>
    /// Defines a collection of extensions for keyboard input.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Retrieves the <see cref="VirtualKey"/> that was found by the <see cref="CharacterReceivedEventArgs"/>.
        /// </summary>
        /// <param name="args">
        /// The <see cref="CharacterReceivedEventArgs"/>.
        /// </param>
        /// <returns>
        /// Returns the <see cref="VirtualKey"/> representation of the <see cref="CharacterReceivedEventArgs"/>
        /// </returns>
        public static VirtualKey VirtualKeyReceived(this CharacterReceivedEventArgs args)
        {
            return (VirtualKey)args.KeyCode;
        }
    }
}