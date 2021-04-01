// <copyright file="IMediatypesResolver.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Files
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatypes resolver.
    /// </summary>
    public interface IMediatypesResolver
    {
        /// <summary>
        /// Resolves file mediatype by its extension.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Matched mediatypes.</returns>
        Task<IList<IMediatype>> ResolveMediatypeAsync(string fileName);
    }
}
