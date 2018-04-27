// <copyright file="IReferenceParseStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Layout.Styles
{
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Reference parse styles data access object.
    /// </summary>
    public interface IReferenceParseStylesDataAccessObject : IDataAccessObject<IReferenceParseStyleDataModel, IReferenceDetailsParseStyleDataModel, IReferenceInsertParseStyleModel, IReferenceUpdateParseStyleModel>
    {
    }
}
