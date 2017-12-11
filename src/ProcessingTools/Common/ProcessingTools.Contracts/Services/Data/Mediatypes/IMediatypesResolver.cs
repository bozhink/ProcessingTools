// <copyright file="IMediatypesResolver.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Mediatypes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Mediatypes;

    /// <summary>
    /// Mediatypes resolver.
    /// </summary>
    public interface IMediatypesResolver
    {
        /// <summary>
        /// Resolves file mediatype by its extension.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>Matched mediatypes.</returns>
        Task<IMediatype[]> ResolveMediatypeAsync(string fileExtension);
    }
}
