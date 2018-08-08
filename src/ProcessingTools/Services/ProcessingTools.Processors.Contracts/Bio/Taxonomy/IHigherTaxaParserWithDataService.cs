// <copyright file="IHigherTaxaParserWithDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

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
