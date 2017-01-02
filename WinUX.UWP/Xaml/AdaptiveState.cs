namespace WinUX.UWP.Xaml
{
    /// <summary>
    /// Defines the enumeration values for an adaptive state's mode.
    /// </summary>
    public enum AdaptiveState
    {
        /// <summary>
        /// An unknown adaptive state.
        /// </summary>
        Unknown,

        /// <summary>
        /// A narrow adaptive state (mobile).
        /// </summary>
        Narrow,

        /// <summary>
        /// A normal adaptive state (desktop/tablet).
        /// </summary>
        Normal,

        /// <summary>
        /// A wide adaptive state (Surface Hub/big screen).
        /// </summary>
        Wide
    }
}