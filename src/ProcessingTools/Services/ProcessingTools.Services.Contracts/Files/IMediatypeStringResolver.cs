// <copyright file="IMediatypeStringResolver.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Files
{
    using System.Threading.Tasks;

    /// <summary>
    /// Mediatype string resolver.
    /// </summary>
    public interface IMediatypeStringResolver
    {
        /// <summary>
        /// Resolves a file extension to corresponding mediatype string.
        /// </summary>
        /// <param name="fileExtension">Lowercase file extension with leading dot. (e.g. '.txt').</param>
        /// <returns>Mediatype string</returns>
        Task<string> ResolveAsync(string fileExtension);
    }
}
