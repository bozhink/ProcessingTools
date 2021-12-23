// <copyright file="IDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Serialization
{
    using System.IO;

    /// <summary>
    /// Generic deserializer.
    /// </summary>
    /// <typeparam name="T">Type of output object.</typeparam>
    public interface IDeserializer<out T>
    {
        /// <summary>
        /// Deserialize string source.
        /// </summary>
        /// <param name="source">String source to be read.</param>
        /// <returns>Deserialized object.</returns>
        T Deserialize(string source);

        /// <summary>
        /// Deserialize stream source.
        /// </summary>
        /// <param name="source">Stream source to be read.</param>
        /// <returns>Deserialized object.</returns>
        T Deserialize(Stream source);
    }
}
