// <copyright file="IFloatObjectParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse styles data service.
    /// </summary>
    public interface IFloatObjectParseStylesDataService : IStylesDataService, IDataService<IFloatObjectParseStyleModel, IFloatObjectDetailsParseStyleModel, IFloatObjectInsertParseStyleModel, IFloatObjectUpdateParseStyleModel>
    {
    }
}
