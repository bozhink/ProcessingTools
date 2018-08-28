// <copyright file="IDecodeService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Tools
{
    using System.Threading.Tasks;

    /// <summary>
    /// Decode service.
    /// </summary>
    public interface IDecodeService
    {
        /// <summary>
        /// Decodes Base64 string to string.
        /// </summary>
        /// <param name="source">Base64 encoded string source to be decoded.</param>
        /// <returns>Decoded source as string.</returns>
        Task<string> DecodeBase64Async(string source);

        /// <summary>
        /// Decodes Base64 URL string to string.
        /// </summary>
        /// <param name="source">Base64 URL encoded string source to be decoded.</param>
        /// <returns>Decoded source as string.</returns>
        Task<string> DecodeBase64UrlAsync(string source);
    }
}
