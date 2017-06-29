namespace WinUX.Data.Serialization
{
    using System;
    using System.Runtime.Serialization.Formatters;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines a data serialization service for JSON.
    /// </summary>
    public sealed class JsonSerializationService : ISerializationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializationService"/> class.
        /// </summary>
        public JsonSerializationService()
            : this(
                new JsonSerializerSettings
                    {
                        Formatting = Formatting.None,
                        TypeNameHandling = TypeNameHandling.Auto,
                        TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                        PreserveReferencesHandling = PreserveReferencesHandling.All,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializationService"/> class.
        /// </summary>
        /// <param name="settings">
        /// The serializer settings.
        /// </param>
        public JsonSerializationService(JsonSerializerSettings settings)
        {
            this.Settings = settings;
        }

        /// <summary>
        /// Gets the JSON serializer settings.
        /// </summary>
        public JsonSerializerSettings Settings { get; }

        /// <summary>
        /// Serializes the given deserialized value.
        /// </summary>
        /// <param name="value">
        /// The value to serialize.
        /// </param>
        /// <returns>
        /// Returns the deserialized object as a <see cref="string"/>.
        /// </returns>
        public string Serialize(object value)
        {
            return this.Serialize(value, SerializationFormatting.None);
        }

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
        public string Serialize(object value, SerializationFormatting formatting)
        {
            if (value == null) return null;

            if (value as string == string.Empty) return string.Empty;

            Formatting jsonFormatting;

            switch (formatting)
            {
                case SerializationFormatting.Indented:
                    jsonFormatting = Formatting.Indented;
                    break;
                default:
                    jsonFormatting = Formatting.None;
                    break;
            }

            return JsonConvert.SerializeObject(value, jsonFormatting, this.Settings);
        }

        /// <summary>
        /// Deserializes the given serialized value to an <see cref="object"/>.
        /// </summary>
        /// <param name="value">
        /// The value to deserialize.
        /// </param>
        /// <returns>
        /// Returns the serialized string as an <see cref="object"/>.
        /// </returns>
        public object Deserialize(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            var obj = JsonConvert.DeserializeObject(value, this.Settings);
            return obj;
        }

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
        public object Deserialize(string value, Type expectedType)
        {
            if (string.IsNullOrEmpty(value)) return null;

            var obj = JsonConvert.DeserializeObject(value, expectedType, this.Settings);
            return obj;
        }

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
        /// Returns the serialized string as a <typeparamref name="T"/>.
        /// </returns>
        public T Deserialize<T>(string value)
        {
            if (string.IsNullOrEmpty(value)) return default(T);

            var obj = JsonConvert.DeserializeObject<T>(value);
            return obj;
        }
    }
}