// <copyright file="IReferenceParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    /// <summary>
    /// Reference parse styles data service.
    /// </summary>
    public interface IReferenceParseStylesDataService : IStylesDataService, IDataService<IReferenceParseStyleModel, IReferenceDetailsParseStyleModel, IReferenceInsertParseStyleModel, IReferenceUpdateParseStyleModel>
    {
    }
}
