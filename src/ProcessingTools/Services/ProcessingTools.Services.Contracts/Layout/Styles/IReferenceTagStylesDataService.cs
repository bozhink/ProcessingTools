// <copyright file="IReferenceTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    /// <summary>
    /// Reference tag styles data service.
    /// </summary>
    public interface IReferenceTagStylesDataService : IStylesDataService, IDataService<IReferenceTagStyleModel, IReferenceDetailsTagStyleModel, IReferenceInsertTagStyleModel, IReferenceUpdateTagStyleModel>
    {
    }
}
