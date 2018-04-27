// <copyright file="IReferenceTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Layout.Styles
{
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles data service.
    /// </summary>
    public interface IReferenceTagStylesDataService : IDataService<IReferenceTagStyleModel, IReferenceDetailsTagStyleModel, IReferenceInsertTagStyleModel, IReferenceUpdateTagStyleModel>
    {
    }
}
