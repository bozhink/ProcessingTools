// <copyright file="ISearchService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISearchService<Tin, Tout> : IService
    {
        Task<IEnumerable<Tout>> Search(Tin filter);
    }
}
