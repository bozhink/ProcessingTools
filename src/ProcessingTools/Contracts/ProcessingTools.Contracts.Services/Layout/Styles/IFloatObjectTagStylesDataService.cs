﻿// <copyright file="IFloatObjectTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object tag styles data service.
    /// </summary>
    public interface IFloatObjectTagStylesDataService : IStylesDataService, IDataService<IFloatObjectTagStyleModel, IFloatObjectDetailsTagStyleModel, IFloatObjectInsertTagStyleModel, IFloatObjectUpdateTagStyleModel>
    {
    }
}
