// <copyright file="IEncodeService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services.Tools
{
    /// <summary>
    /// Encode service.
    /// </summary>
    public interface IEncodeService
    {
        /// <summary>
        /// Encodes string to Base64 string.
        /// </summary>
        /// <param name="source">String source to be encoded.</param>
        /// <returns>Encoded source string as Base64 string.</returns>
        Task<string> EncodeBase64Async(string source);

        /// <summary>
        /// Encodes string to Base64 URL string.
        /// </summary>
        /// <param name="source">String source to be encoded.</param>
        /// <returns>Encoded source string as Base64 URL string.</returns>
        Task<string> EncodeBase64UrlAsync(string source);
    }
}
