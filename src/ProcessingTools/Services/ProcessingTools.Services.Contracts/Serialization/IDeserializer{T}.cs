// <copyright file="IDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.IO;
using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services.Serialization
{
    /// <summary>
    /// Generic deserializer.
    /// </summary>
    /// <typeparam name="T">Type of output object.</typeparam>
    public interface IDeserializer<T>
    {
        /// <summary>
        /// Asynchronously deserialize stream.
        /// </summary>
        /// <param name="stream">Stream to be read.</param>
        /// <returns>Task of output type.</returns>
        Task<T> DeserializeAsync(Stream stream);
    }
}
