// <copyright file="IReferenceTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles data service.
    /// </summary>
    public interface IReferenceTagStylesDataService : IStylesDataService, IDataService<IReferenceTagStyleModel, IReferenceDetailsTagStyleModel, IReferenceInsertTagStyleModel, IReferenceUpdateTagStyleModel>
    {
    }
}
