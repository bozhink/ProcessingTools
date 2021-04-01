// <copyright file="IReferenceTagStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Styles
{
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles data access object (DAO).
    /// </summary>
    public interface IReferenceTagStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IReferenceTagStyleDataTransferObject, IReferenceDetailsTagStyleDataTransferObject, IReferenceInsertTagStyleModel, IReferenceUpdateTagStyleModel>
    {
    }
}
