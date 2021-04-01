// <copyright file="IProvincesDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Provinces data service.
    /// </summary>
    public interface IProvincesDataService : IDataServiceAsync<IProvince, IProvincesFilter>, IGeoSynonymisableDataService<IProvince, IProvinceSynonym, IProvinceSynonymsFilter>
    {
    }
}
