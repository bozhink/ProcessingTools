// <copyright file="IHasher.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Hasher: hashing utility.
    /// </summary>
    public interface IHasher
    {
        /// <summary>
        /// Compute the hash of source string in HEX format.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Hash string in HEX format.</returns>
        string ComputeHash(string source);

        /// <summary>
        /// Compute the hash of source string in Base64 format.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Hash string in Base64 format.</returns>
        string ComputeHashBase64(string source);
    }
}
