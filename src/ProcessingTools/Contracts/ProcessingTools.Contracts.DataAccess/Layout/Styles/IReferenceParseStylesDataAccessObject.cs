// <copyright file="IReferenceParseStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Styles
{
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Reference parse styles data access object.
    /// </summary>
    public interface IReferenceParseStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IReferenceParseStyleDataModel, IReferenceDetailsParseStyleDataModel, IReferenceInsertParseStyleModel, IReferenceUpdateParseStyleModel>
    {
    }
}
