// <copyright file="IFloatObjectTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Layout.Styles.Floats;

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    /// <summary>
    /// Float object tag styles data service.
    /// </summary>
    public interface IFloatObjectTagStylesDataService : IStylesDataService, IDataService<IFloatObjectTagStyleModel, IFloatObjectDetailsTagStyleModel, IFloatObjectInsertTagStyleModel, IFloatObjectUpdateTagStyleModel>
    {
    }
}
