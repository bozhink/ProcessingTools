// <copyright file="ISerializer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Serialization
{
    using System.IO;

    /// <summary>
    /// Serializer.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize source object to string.
        /// </summary>
        /// <param name="source">Source object to be serialized.</param>
        /// <returns>Serialized string.</returns>
        string SerializeToString(object source);

        /// <summary>
        /// Serialize source object to stream.
        /// </summary>
        /// <param name="source">Source object to be serialized.</param>
        /// <returns>Serialized stream.</returns>
        Stream SerializeToStream(object source);
    }
}
