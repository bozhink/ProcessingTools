// <copyright file="ITypeStatusDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Bio
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Contracts.Services.Models.Data.Bio;

    /// <summary>
    /// Types statuses data service.
    /// </summary>
    public interface ITypeStatusDataService : IMultiDataServiceAsync<ITypeStatus, IFilter>
    {
    }
}
