// <copyright file="IReferenceTagStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Layout.Styles
{
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles data access object.
    /// </summary>
    public interface IReferenceTagStylesDataAccessObject : IDataAccessObject<IReferenceTagStyleDataModel, IReferenceDetailsTagStyleDataModel, IReferenceInsertTagStyleModel, IReferenceUpdateTagStyleModel>
    {
    }
}
