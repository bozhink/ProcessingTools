// <copyright file="NewtonsoftJsonDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Serialization
{
    using System.IO;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Serialization;

    /// <summary>
    /// <see cref="Newtonsoft"/> JSON Deserializer.
    /// </summary>
    public class NewtonsoftJsonDeserializer : IJsonDeserializer
    {
        /// <summary>
        /// Gets or sets the <see cref="JsonSerializerSettings"/>.
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        /// <inheritdoc/>
        public T Deserialize<T>(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(source, this.JsonSerializerSettings);
        }

        /// <inheritdoc/>
        public T Deserialize<T>(Stream source)
        {
            if (source is null || !source.CanRead)
            {
                return default;
            }

            using (var reader = new StreamReader(source, Defaults.Encoding, true, 4096, true))
            {
                return this.Deserialize<T>(reader.ReadToEnd());
            }
        }
    }
}
