// <copyright file="IDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// De-serializer
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Asynchronously deserialize stream.
        /// </summary>
        /// <typeparam name="T">Type of output object</typeparam>
        /// <param name="stream">Stream to be read</param>
        /// <returns>Task of output type</returns>
        Task<T> DeserializeAsync<T>(Stream stream);
    }
}
