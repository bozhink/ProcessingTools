// <copyright file="DataContractJsonDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Serialization
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Serialization;

    /// <summary>
    /// Data contract JSON deserializer.
    /// </summary>
    public class DataContractJsonDeserializer : IJsonDeserializer
    {
        /// <inheritdoc/>
        public T Deserialize<T>(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default;
            }

            var bytes = Defaults.Encoding.GetBytes(source);
            using (var stream = new MemoryStream(bytes))
            {
                return this.Deserialize<T>(stream);
            }
        }

        /// <inheritdoc/>
        public T Deserialize<T>(Stream source)
        {
            if (source is null || !source.CanRead)
            {
                return default;
            }

            var serializer = new DataContractJsonSerializer(typeof(T));

            var result = serializer.ReadObject(source);

            return (T)result;
        }
    }
}
