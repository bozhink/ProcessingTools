// <copyright file="IInstitutionsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Resources
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Institutions data service.
    /// </summary>
    public interface IInstitutionsDataService : ISelectableDataServiceAsync<IInstitution, IFilter>
    {
    }
}
