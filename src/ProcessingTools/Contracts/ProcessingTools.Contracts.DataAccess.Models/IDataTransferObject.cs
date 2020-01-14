// <copyright file="IDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Data transfer object (DTO).
    /// </summary>
    public interface IDataTransferObject : IIdentifiedDataTransferObject, ICreated, IModified
    {
    }
}
