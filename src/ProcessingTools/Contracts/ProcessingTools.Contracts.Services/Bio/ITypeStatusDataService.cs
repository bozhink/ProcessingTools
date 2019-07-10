// <copyright file="ITypeStatusDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;
using ProcessingTools.Contracts.Services.Models.Bio;

namespace ProcessingTools.Contracts.Services.Bio
{
    /// <summary>
    /// Types statuses data service.
    /// </summary>
    public interface ITypeStatusDataService : ISelectableDataServiceAsync<ITypeStatus, IFilter>
    {
    }
}
