// <copyright file="IMorphologicalEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Bio
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Contracts.Services.Models.Data.Bio;

    /// <summary>
    /// Morphological epithets data service.
    /// </summary>
    public interface IMorphologicalEpithetsDataService : IMultiDataServiceAsync<IMorphologicalEpithet, IFilter>
    {
    }
}
