// <copyright file="IDataModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Models
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Data model.
    /// </summary>
    public interface IDataModel : IStringIdentified, IObjectIdentified, ICreated, IModified
    {
    }
}
