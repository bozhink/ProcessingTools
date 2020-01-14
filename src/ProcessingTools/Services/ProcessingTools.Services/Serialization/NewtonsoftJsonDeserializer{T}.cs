// <copyright file="NewtonsoftJsonDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Serialization
{
    using System.IO;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Serialization;

    /// <summary>
    /// Generic <see cref="Newtonsoft"/> JSON Deserializer.
    /// </summary>
    /// <typeparam name="T">Type of serialization model.</typeparam>
    public class NewtonsoftJsonDeserializer<T> : IJsonDeserializer<T>
    {
        /// <summary>
        /// Gets or sets the <see cref="JsonSerializerSettings"/>.
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        /// <inheritdoc/>
        public T Deserialize(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(source, this.JsonSerializerSettings);
        }

        /// <inheritdoc/>
        public T Deserialize(Stream source)
        {
            if (source is null || !source.CanRead)
            {
                return default;
            }

            using (var reader = new StreamReader(source, Defaults.Encoding, true, 4096, true))
            {
                return this.Deserialize(reader.ReadToEnd());
            }
        }
    }
}
