// <copyright file="DataContractJsonDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Serialization
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Serialization;

    /// <summary>
    /// Generic data contract JSON deserializer.
    /// </summary>
    /// <typeparam name="T">Type of serialization model.</typeparam>
    public class DataContractJsonDeserializer<T> : IDataContractJsonDeserializer<T>
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
        public Task<T> DeserializeAsync(Stream stream)
        {
            return Task.Run(() =>
            {
                if (stream == null || !stream.CanRead)
                {
                    return default(T);
                }

                var result = (T)this.serializer.ReadObject(stream);
                return result;
            });
        }
    }
}
