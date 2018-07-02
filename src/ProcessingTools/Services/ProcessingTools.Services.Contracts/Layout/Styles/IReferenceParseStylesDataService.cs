// <copyright file="IReferenceParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Layout.Styles
{
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Reference parse styles data service.
    /// </summary>
    public interface IReferenceParseStylesDataService : IStylesDataService, IDataService<IReferenceParseStyleModel, IReferenceDetailsParseStyleModel, IReferenceInsertParseStyleModel, IReferenceUpdateParseStyleModel>
    {
    }
}
