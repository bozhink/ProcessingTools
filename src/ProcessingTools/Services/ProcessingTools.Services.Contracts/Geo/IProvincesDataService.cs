// <copyright file="IProvincesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Provinces data service.
    /// </summary>
    public interface IProvincesDataService : IDataServiceAsync<IProvince, IProvincesFilter>, IGeoSynonymisableDataService<IProvince, IProvinceSynonym, IProvinceSynonymsFilter>
    {
    }
}
