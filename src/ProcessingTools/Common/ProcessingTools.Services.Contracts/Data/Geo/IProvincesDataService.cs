// <copyright file="IProvincesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IProvincesDataService : IDataServiceAsync<IProvince, IProvincesFilter>, IGeoSynonymisableDataService<IProvince, IProvinceSynonym, IProvinceSynonymsFilter>
    {
    }
}
