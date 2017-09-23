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
