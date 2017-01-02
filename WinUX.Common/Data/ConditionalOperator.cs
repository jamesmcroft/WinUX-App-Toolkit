namespace WinUX.Data
{
    /// <summary>
    /// Defines the enumeration values for conditional operators.
    /// </summary>
    public enum ConditionalOperator
    {
        /// <summary>
        /// Checks whether the left value is equal to the right.
        /// </summary>
        EqualToRight = 0,

        /// <summary>
        /// Checks whether the left value is not equal to the right.
        /// </summary>
        NotEqualToRight = 1,

        /// <summary>
        /// Checks whether the left value is less than the right.
        /// </summary>
        LessThanRight = 2,

        /// <summary>
        /// Checks whether the left value is less than or equal to the right.
        /// </summary>
        LessThanOrEqualToRight = 3,

        /// <summary>
        /// Checks whether the left value is greater than the right.
        /// </summary>
        GreaterThanRight = 4,

        /// <summary>
        /// Checks whether the left value is greater than or equal to the right.
        /// </summary>
        GreaterThanOrEqualToRight = 5,

        /// <summary>
        /// Checks whether the value is null.
        /// </summary>
        IsNull = 6,

        /// <summary>
        /// Checks whether the value is not null.
        /// </summary>
        IsNotNull = 7,

        /// <summary>
        /// Checks whether the condition is true.
        /// </summary>
        IsTrue = 8,

        /// <summary>
        /// Checks whether the condition is false.
        /// </summary>
        IsFalse = 9,

        /// <summary>
        /// Checks whether the value is null or empty.
        /// </summary>
        IsNullOrEmpty = 10,

        /// <summary>
        /// Checks whether the value is not null or empty.
        /// </summary>
        IsNotNullOrEmpty = 11
    }
}