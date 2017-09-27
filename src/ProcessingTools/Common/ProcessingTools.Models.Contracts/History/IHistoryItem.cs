// <copyright file="IHistoryItem.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.History
{
    using System;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// History item.
    /// </summary>
    public interface IHistoryItem : IStringIdentifiable
    {
        /// <summary>
        /// Gets data.
        /// </summary>
        string Data { get; }

        /// <summary>
        /// Gets date modified.
        /// </summary>
        DateTime DateModified { get; }

        /// <summary>
        /// Gets object ID.
        /// </summary>
        string ObjectId { get; }

        /// <summary>
        /// Gets object type.
        /// </summary>
        string ObjectType { get; }

        /// <summary>
        /// Gets user ID.
        /// </summary>
        string UserId { get; }
    }
}
