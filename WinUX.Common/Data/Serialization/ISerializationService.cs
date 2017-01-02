namespace WinUX.Data.Serialization
{
    using System;

    /// <summary>
    /// Defines an interface for a data serialization service.
    /// </summary>
    public interface ISerializationService
    {
        /// <summary>
        /// Serializes the given deserialized value.
        /// </summary>
        /// <param name="value">
        /// The value to serialize.
        /// </param>
        /// <returns>
        /// Returns the deserialized object as a <see cref="string"/>.
        /// </returns>
        string Serialize(object value);

        /// <summary>
        /// Serializes the given deserialized value.
        /// </summary>
        /// <param name="value">
        /// The value to serialize.
        /// </param>
        /// <param name="formatting">
        /// The formatting.
        /// </param>
        /// <returns>
        /// Returns the deserialized object as a <see cref="string"/>.
        /// </returns>
        string Serialize(object value, SerializationFormatting formatting);

        /// <summary>
        /// Deserializes the given serialized value to an <see cref="object"/>.
        /// </summary>
        /// <param name="value">
        /// The value to deserialize.
        /// </param>
        /// <returns>
        /// Returns the serialized string as an <see cref="object"/>.
        /// </returns>
        object Deserialize(string value);

        /// <summary>
        /// Deserializes the given serialized value to an <see cref="object"/>.
        /// </summary>
        /// <param name="value">
        /// The value to deserialize.
        /// </param>
        /// <param name="expectedType">
        /// The expected Type.
        /// </param>
        /// <returns>
        /// Returns the serialized string as an <see cref="object"/>.
        /// </returns>
        object Deserialize(string value, Type expectedType);

        /// <summary>
        /// Deserializes the given serialized value to a specific type.
        /// </summary>
        /// <typeparam name="T">
        /// The type to deserialize to.
        /// </typeparam>
        /// <param name="value">
        /// The value to deserialize.
        /// </param>
        /// <returns>
        /// Returns the serialized string as a <see cref="T"/>.
        /// </returns>
        T Deserialize<T>(string value);
    }
}