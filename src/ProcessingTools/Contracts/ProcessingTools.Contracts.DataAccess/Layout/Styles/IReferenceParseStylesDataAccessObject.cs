// <copyright file="IReferenceParseStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Styles
{
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Reference parse styles data access object (DAO).
    /// </summary>
    public interface IReferenceParseStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IReferenceParseStyleDataTransferObject, IReferenceDetailsParseStyleDataTransferObject, IReferenceInsertParseStyleModel, IReferenceUpdateParseStyleModel>
    {
    }
}
