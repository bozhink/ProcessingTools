﻿// <copyright file="IMediatypesResolver.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Files
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

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
        Task<IList<IMediatype>> ResolveMediatypeAsync(string fileExtension);
    }
}
