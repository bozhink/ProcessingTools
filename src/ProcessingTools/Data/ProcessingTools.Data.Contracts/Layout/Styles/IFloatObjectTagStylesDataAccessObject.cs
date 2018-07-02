// <copyright file="IFloatObjectTagStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Layout.Styles
{
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Models.Contracts.Layout.Styles.Floats;

    /// <summary>
    /// Float object tag styles data access object.
    /// </summary>
    public interface IFloatObjectTagStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IFloatObjectTagStyleDataModel, IFloatObjectDetailsTagStyleDataModel, IFloatObjectInsertTagStyleModel, IFloatObjectUpdateTagStyleModel>
    {
    }
}
