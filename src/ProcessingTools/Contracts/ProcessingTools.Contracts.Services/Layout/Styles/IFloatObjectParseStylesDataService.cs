// <copyright file="IFloatObjectParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Layout.Styles.Floats;

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    /// <summary>
    /// Float object parse styles data service.
    /// </summary>
    public interface IFloatObjectParseStylesDataService : IStylesDataService, IDataService<IFloatObjectParseStyleModel, IFloatObjectDetailsParseStyleModel, IFloatObjectInsertParseStyleModel, IFloatObjectUpdateParseStyleModel>
    {
    }
}
