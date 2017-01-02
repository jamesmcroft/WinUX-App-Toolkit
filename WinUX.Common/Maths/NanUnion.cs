namespace WinUX.Maths
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Defines a model for a union of NaN values.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct NanUnion
    {
        /// <summary>
        /// Floating point representation of the union.
        /// </summary>
        [FieldOffset(0)]
        internal double FloatingValue;

        /// <summary>
        /// Integer representation of the union.
        /// </summary>
        [FieldOffset(0)]
        internal ulong IntegerValue;
    }
}