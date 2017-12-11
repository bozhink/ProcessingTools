// <copyright file="IInstitutionsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Institutions data service.
    /// </summary>
    public interface IInstitutionsDataService : IMultiDataServiceAsync<IInstitution, IFilter>
    {
    }
}
