// <copyright file="IMorphologicalEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Services.Data.Bio;

    /// <summary>
    /// Morphological epithets data service.
    /// </summary>
    public interface IMorphologicalEpithetsDataService : IMultiDataServiceAsync<IMorphologicalEpithet, IFilter>
    {
    }
}
