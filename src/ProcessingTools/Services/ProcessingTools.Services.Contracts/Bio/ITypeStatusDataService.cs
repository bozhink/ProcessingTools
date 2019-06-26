// <copyright file="ITypeStatusDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Services.Models.Contracts.Bio;

    /// <summary>
    /// Types statuses data service.
    /// </summary>
    public interface ITypeStatusDataService : ISelectableDataServiceAsync<ITypeStatus, IFilter>
    {
    }
}
