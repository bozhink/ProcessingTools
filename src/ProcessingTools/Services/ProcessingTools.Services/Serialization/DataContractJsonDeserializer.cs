﻿// <copyright file="DataContractJsonDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using ProcessingTools.Contracts.Services.Serialization;

namespace ProcessingTools.Services.Serialization
{
    /// <summary>
    /// Data contract JSON deserializer.
    /// </summary>
    public class DataContractJsonDeserializer : IDataContractJsonDeserializer
    {
        /// <inheritdoc/>
        public Task<T> DeserializeAsync<T>(Stream stream)
        {
            return Task.Run(() =>
            {
                if (stream == null || !stream.CanRead)
                {
                    return default(T);
                }

                var serializer = new DataContractJsonSerializer(typeof(T));
                var result = (T)serializer.ReadObject(stream);
                return result;
            });
        }
    }
}
