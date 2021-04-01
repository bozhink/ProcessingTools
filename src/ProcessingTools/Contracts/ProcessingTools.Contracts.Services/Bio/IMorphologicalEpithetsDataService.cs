// <copyright file="IMorphologicalEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Bio;

    /// <summary>
    /// Morphological epithets data service.
    /// </summary>
    public interface IMorphologicalEpithetsDataService : ISelectableDataServiceAsync<IMorphologicalEpithet, IFilter>
    {
    }
}
