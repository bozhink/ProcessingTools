// <copyright file="IIdentifiedDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Identified data transfer object (DTO).
    /// </summary>
    public interface IIdentifiedDataTransferObject : IStringIdentified, IObjectIdentified
    {
    }
}
