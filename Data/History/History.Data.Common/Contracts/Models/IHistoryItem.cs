namespace ProcessingTools.History.Data.Common.Contracts.Models
{
    using System;
    using ProcessingTools.Contracts.Models;

    public interface IHistoryItem : IStringIdentifiable
    {
        string Data { get; }

        DateTime DateModified { get; }

        string ObjectId { get; }

        string ObjectType { get; }

        string UserId { get; }
    }
}
