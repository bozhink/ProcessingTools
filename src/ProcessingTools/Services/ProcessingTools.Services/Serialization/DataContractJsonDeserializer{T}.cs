// <copyright file="DataContractJsonDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Serialization
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Serialization;

    /// <summary>
    /// Generic data contract JSON deserializer.
    /// </summary>
    /// <typeparam name="T">Type of serialization model.</typeparam>
    public class DataContractJsonDeserializer<T> : IJsonDeserializer<T>
    {
        private readonly DataContractJsonSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonDeserializer{T}"/> class.
        /// </summary>
        public DataContractJsonDeserializer()
        {
            this.serializer = new DataContractJsonSerializer(typeof(T));
        }

        /// <inheritdoc/>
        public T Deserialize(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default;
            }

            var bytes = Defaults.Encoding.GetBytes(source);
            using (var stream = new MemoryStream(bytes))
            {
                return this.Deserialize(stream);
            }
        }

        /// <inheritdoc/>
        public T Deserialize(Stream source)
        {
            if (source is null || !source.CanRead)
            {
                return default;
            }

            var result = this.serializer.ReadObject(source);

            return (T)result;
        }
    }
}
