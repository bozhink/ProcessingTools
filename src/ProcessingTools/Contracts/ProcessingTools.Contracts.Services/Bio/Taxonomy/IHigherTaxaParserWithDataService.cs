// <copyright file="IHigherTaxaParserWithDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Higher taxa parser with data service.
    /// </summary>
    /// <typeparam name="TService">Type of data service.</typeparam>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IHigherTaxaParserWithDataService<TService, T> : IXmlContextParser<long>
        where TService : ITaxonRankResolver
        where T : ITaxonRank
    {
    }
}
