// <copyright file="ITreatmentMetaParserWithDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Contracts;

    /// <summary>
    /// Treatment meta parser with data service.
    /// </summary>
    /// <typeparam name="TService">Type of data service.</typeparam>
    public interface ITreatmentMetaParserWithDataService<TService> : ITreatmentMetaParser
        where TService : ITaxonClassificationResolver
    {
    }
}
