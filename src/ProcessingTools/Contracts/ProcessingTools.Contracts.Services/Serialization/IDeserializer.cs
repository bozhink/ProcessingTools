// <copyright file="IDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Serialization
{
    using System.IO;

    /// <summary>
    /// Deserializer.
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Deserialize string source.
        /// </summary>
        /// <typeparam name="T">Type of output object.</typeparam>
        /// <param name="source">String source to be read.</param>
        /// <returns>Deserialized object.</returns>
        T Deserialize<T>(string source);

        /// <summary>
        /// Deserialize stream source.
        /// </summary>
        /// <typeparam name="T">Type of output object.</typeparam>
        /// <param name="source">Stream source to be read.</param>
        /// <returns>Deserialized object.</returns>
        T Deserialize<T>(Stream source);
    }
}
