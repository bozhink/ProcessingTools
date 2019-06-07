// <copyright file="IPostCodesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Post codes data service.
    /// </summary>
    public interface IPostCodesDataService : IDataServiceAsync<IPostCode, IPostCodesFilter>
    {
    }
}
