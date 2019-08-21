// <copyright file="ITypeStatusDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Bio;

    /// <summary>
    /// Types statuses data service.
    /// </summary>
    public interface ITypeStatusDataService : ISelectableDataServiceAsync<ITypeStatus, IFilter>
    {
    }
}
