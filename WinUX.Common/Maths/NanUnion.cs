namespace WinUX.Maths
{
    using System.Runtime.InteropServices;
    
    /// <summary>
    /// Defines a C++ style type union used for efficiently converting a double into an unsigned long, whose bits can be easily manipulated.
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