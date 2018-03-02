// <copyright file="ITypeStatusDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Models.Contracts.Bio;

    /// <summary>
    /// Types statuses data service.
    /// </summary>
    public interface ITypeStatusDataService : IMultiDataServiceAsync<ITypeStatus, IFilter>
    {
    }
}
