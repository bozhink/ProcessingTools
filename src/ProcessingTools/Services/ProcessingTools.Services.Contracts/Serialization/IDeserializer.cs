// <copyright file="IDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.IO;
using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services.Serialization
{
    /// <summary>
    /// Deserializer.
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Asynchronously deserialize stream.
        /// </summary>
        /// <typeparam name="T">Type of output object.</typeparam>
        /// <param name="stream">Stream to be read.</param>
        /// <returns>Task of output type.</returns>
        Task<T> DeserializeAsync<T>(Stream stream);
    }
}
