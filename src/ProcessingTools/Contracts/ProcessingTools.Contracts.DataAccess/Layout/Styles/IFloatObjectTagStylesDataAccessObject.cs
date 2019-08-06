// <copyright file="IFloatObjectTagStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Styles
{
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object tag styles data access object (DAO).
    /// </summary>
    public interface IFloatObjectTagStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IFloatObjectTagStyleDataTransferObject, IFloatObjectDetailsTagStyleDataTransferObject, IFloatObjectInsertTagStyleModel, IFloatObjectUpdateTagStyleModel>
    {
    }
}
