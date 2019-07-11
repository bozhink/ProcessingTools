// <copyright file="IReferenceParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;

    /// <summary>
    /// Reference parse styles data service.
    /// </summary>
    public interface IReferenceParseStylesDataService : IStylesDataService, IDataService<IReferenceParseStyleModel, IReferenceDetailsParseStyleModel, IReferenceInsertParseStyleModel, IReferenceUpdateParseStyleModel>
    {
    }
}
