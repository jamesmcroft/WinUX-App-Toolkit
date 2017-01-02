namespace WinUX.Data.Serialization
{
    /// <summary>
    /// Defines a data serialization service for multiple serialization types.
    /// </summary>
    public static class SerializationService
    {
        private static ISerializationService json;

        /// <summary>
        /// Gets a JSON serialization service.
        /// </summary>
        public static ISerializationService Json => json ?? (json = new JsonSerializationService());
    }
}