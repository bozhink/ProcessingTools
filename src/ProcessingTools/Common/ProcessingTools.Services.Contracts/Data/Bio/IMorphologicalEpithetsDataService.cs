// <copyright file="IMorphologicalEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Data.Bio
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts.Data;
    using ProcessingTools.Services.Models.Contracts.Data.Bio;

    /// <summary>
    /// Morphological epithets data service.
    /// </summary>
    public interface IMorphologicalEpithetsDataService : IMultiDataServiceAsync<IMorphologicalEpithet, IFilter>
    {
    }
}
