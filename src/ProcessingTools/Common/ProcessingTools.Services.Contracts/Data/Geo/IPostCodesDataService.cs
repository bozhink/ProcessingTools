// <copyright file="IPostCodesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Data.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Post codes data service.
    /// </summary>
    public interface IPostCodesDataService : IDataServiceAsync<IPostCode, IPostCodesFilter>
    {
    }
}
