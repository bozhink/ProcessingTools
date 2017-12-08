// <copyright file="IMediatypesResolver.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Data.Contracts.Mediatypes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Mediatypes;

    public interface IMediatypesResolver
    {
        Task<IEnumerable<IMediatype>> ResolveMediatype(string fileExtension);
    }
}
