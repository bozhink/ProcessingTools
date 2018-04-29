// <copyright file="IFloatObjectParseStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Layout.Styles
{
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Models.Contracts.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse styles data access object.
    /// </summary>
    public interface IFloatObjectParseStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IFloatObjectParseStyleDataModel, IFloatObjectDetailsParseStyleDataModel, IFloatObjectInsertParseStyleModel, IFloatObjectUpdateParseStyleModel>
    {
    }
}
