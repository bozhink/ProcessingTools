// <copyright file="IDataModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Data model.
    /// </summary>
    public interface IDataModel : IIdentifiedDataModel, ICreated, IModified
    {
    }
}
