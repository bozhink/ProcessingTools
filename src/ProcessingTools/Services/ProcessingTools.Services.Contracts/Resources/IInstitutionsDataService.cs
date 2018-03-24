// <copyright file="IInstitutionsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Resources
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;

    /// <summary>
    /// Institutions data service.
    /// </summary>
    public interface IInstitutionsDataService : IMultiDataServiceAsync<IInstitution, IFilter>
    {
    }
}
