// <copyright file="IIdentifiedDataModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Contracts
{
    using System;
    using ProcessingTools.Models.Contracts;

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
