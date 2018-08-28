// <copyright file="IEncodeService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Tools
{
    using System.Threading.Tasks;

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
