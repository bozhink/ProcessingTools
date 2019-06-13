// <copyright file="IFloatObjectParseStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Styles
{
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse styles data access object.
    /// </summary>
    public interface IFloatObjectParseStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IFloatObjectParseStyleDataTransferObject, IFloatObjectDetailsParseStyleDataTransferObject, IFloatObjectInsertParseStyleModel, IFloatObjectUpdateParseStyleModel>
    {
    }
}
