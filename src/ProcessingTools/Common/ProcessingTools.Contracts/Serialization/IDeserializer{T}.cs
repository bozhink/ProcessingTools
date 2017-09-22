// <copyright file="IDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Serialization
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic deserializer.
    /// </summary>
    /// <typeparam name="T">Type of output object</typeparam>
    public interface IDeserializer<T>
    {
        /// <summary>
        /// Asynchronously deserialize stream.
        /// </summary>
        /// <param name="stream">Stream to be read</param>
        /// <returns>Task of output type</returns>
        Task<T> DeserializeAsync(Stream stream);
    }
}
