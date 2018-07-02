// <copyright file="IFloatObjectTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Layout.Styles
{
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats;

    /// <summary>
    /// Float object tag styles data service.
    /// </summary>
    public interface IFloatObjectTagStylesDataService : IStylesDataService, IDataService<IFloatObjectTagStyleModel, IFloatObjectDetailsTagStyleModel, IFloatObjectInsertTagStyleModel, IFloatObjectUpdateTagStyleModel>
    {
    }
}
