// <copyright file="ITreatmentMetaParserWithDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Treatment meta parser with data service.
    /// </summary>
    /// <typeparam name="TService">Type of data service.</typeparam>
    public interface ITreatmentMetaParserWithDataService<TService> : ITreatmentMetaParser
        where TService : ITaxonClassificationResolver
    {
    }
}
