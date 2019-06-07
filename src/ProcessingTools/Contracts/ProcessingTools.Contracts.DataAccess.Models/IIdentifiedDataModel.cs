// <copyright file="IIdentifiedDataModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models
{
    using System;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Identified data model.
    /// </summary>
    public interface IIdentifiedDataModel : IStringIdentifiable
    {
        /// <summary>
        /// Gets the object ID.
        /// </summary>
        Guid ObjectId { get; }
    }
}
