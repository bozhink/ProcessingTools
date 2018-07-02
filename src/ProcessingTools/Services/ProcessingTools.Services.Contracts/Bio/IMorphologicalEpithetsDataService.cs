// <copyright file="IMorphologicalEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Models.Contracts.Bio;

    /// <summary>
    /// Morphological epithets data service.
    /// </summary>
    public interface IMorphologicalEpithetsDataService : IMultiDataServiceAsync<IMorphologicalEpithet, IFilter>
    {
    }
}
