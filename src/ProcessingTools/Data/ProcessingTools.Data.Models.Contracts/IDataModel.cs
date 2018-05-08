// <copyright file="IDataModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Contracts
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Data model.
    /// </summary>
    public interface IDataModel : IIdentifiedDataModel, ICreated, IModified
    {
    }
}
