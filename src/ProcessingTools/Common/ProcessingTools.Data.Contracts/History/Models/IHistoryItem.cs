// <copyright file="IHistoryItem.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.History.Models
{
    using System;
    using ProcessingTools.Models.Contracts;

    public interface IHistoryItem : IStringIdentifiable
    {
        string Data { get; }

        DateTime DateModified { get; }

        string ObjectId { get; }

        string ObjectType { get; }

        string UserId { get; }
    }
}
