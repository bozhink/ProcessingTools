// <copyright file="IReferenceTagStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Layout.Styles
{
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles data access object.
    /// </summary>
    public interface IReferenceTagStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IReferenceTagStyleDataModel, IReferenceDetailsTagStyleDataModel, IReferenceInsertTagStyleModel, IReferenceUpdateTagStyleModel>
    {
    }
}
