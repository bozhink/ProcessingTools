// <copyright file="IHashService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Tools
{
    using System.Threading.Tasks;

    /// <summary>
    /// Hash service.
    /// </summary>
    public interface IHashService
    {
        /// <summary>
        /// Gets MD5 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated MD5 hash as string.</returns>
        Task<string> GetMD5HashAsStringAsync(string source);

        /// <summary>
        /// Gets MD5 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated MD5 hash as Base 64 string.</returns>
        Task<string> GetMD5HashAsBase64StringAsync(string source);

        /// <summary>
        /// Calculates SHA1 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA1 hash as string.</returns>
        Task<string> GetSHA1HashAsStringAsync(string source);

        /// <summary>
        /// Calculates SHA1 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA1 hash as Base 64 string.</returns>
        Task<string> GetSHA1HashAsBase64StringAsync(string source);

        /// <summary>
        /// Gets SHA256 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA256 hash as string.</returns>
        Task<string> GetSHA256HashAsStringAsync(string source);

        /// <summary>
        /// Gets SHA256 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA256 hash as Base 64 string.</returns>
        Task<string> GetSHA256HashAsBase64StringAsync(string source);

        /// <summary>
        /// Gets SHA384 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA384 hash as string.</returns>
        Task<string> GetSHA384HashAsStringAsync(string source);

        /// <summary>
        /// Gets SHA384 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA384 hash as Base 64 string.</returns>
        Task<string> GetSHA384HashAsBase64StringAsync(string source);

        /// <summary>
        /// Gets SHA512 hash as string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA512 hash as string.</returns>
        Task<string> GetSHA512HashAsStringAsync(string source);

        /// <summary>
        /// Gets SHA512 hash as Base 64 string.
        /// </summary>
        /// <param name="source">Source string to be evaluated.</param>
        /// <returns>Evaluated SHA512 hash as Base 64 string.</returns>
        Task<string> GetSHA512HashAsBase64StringAsync(string source);
    }
}
