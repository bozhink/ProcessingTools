// <copyright file="IMediatypeStringResolver.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Data.Contracts.Mediatypes
{
    using System.Threading.Tasks;

    public interface IMediatypeStringResolver
    {
        /// <summary>
        /// Resolves a file extension to corresponding mediatype string.
        /// </summary>
        /// <param name="fileExtension">Lowercase file extension with leading dot. (e.g. '.txt').</param>
        /// <returns>Mediatype string</returns>
        Task<string> Resolve(string fileExtension);
    }
}
