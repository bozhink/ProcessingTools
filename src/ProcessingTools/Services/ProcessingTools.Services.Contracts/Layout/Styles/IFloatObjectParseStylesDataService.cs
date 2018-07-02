// <copyright file="IFloatObjectParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Layout.Styles
{
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse styles data service.
    /// </summary>
    public interface IFloatObjectParseStylesDataService : IStylesDataService, IDataService<IFloatObjectParseStyleModel, IFloatObjectDetailsParseStyleModel, IFloatObjectInsertParseStyleModel, IFloatObjectUpdateParseStyleModel>
    {
    }
}
