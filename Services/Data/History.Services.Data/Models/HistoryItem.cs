namespace ProcessingTools.History.Services.Data.Models
{
    using System;
    using ProcessingTools.History.Data.Common.Contracts.Models;

    internal class HistoryItem : IHistoryItem
    {
        public HistoryItem()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateModified = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public string Data { get; set; }

        public DateTime DateModified { get; set; }

        public string ObjectId { get; set; }

        public string ObjectType { get; set; }

        public string UserId { get; set; }
    }
}
