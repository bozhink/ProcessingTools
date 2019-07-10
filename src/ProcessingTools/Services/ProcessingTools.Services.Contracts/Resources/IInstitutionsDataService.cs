// <copyright file="IInstitutionsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;
using ProcessingTools.Contracts.Models.Resources;

namespace ProcessingTools.Contracts.Services.Resources
{
    /// <summary>
    /// Institutions data service.
    /// </summary>
    public interface IInstitutionsDataService : ISelectableDataServiceAsync<IInstitution, IFilter>
    {
    }
}
