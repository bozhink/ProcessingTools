// <copyright file="ITaxaInformationResolver.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITaxaInformationResolver<T>
    {
        Task<IEnumerable<T>> Resolve(params string[] scientificNames);
    }
}
