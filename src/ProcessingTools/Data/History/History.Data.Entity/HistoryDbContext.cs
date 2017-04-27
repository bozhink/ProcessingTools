namespace ProcessingTools.History.Data.Entity
{
    using System.Data.Entity;
    using Contracts;
    using Models;
    using ProcessingTools.Data.Common.Entity;

    public class HistoryDbContext : EntityDbContext, IHistoryDbContext
    {
        public HistoryDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<HistoryItem> HistoryItems { get; set; }
    }
}
