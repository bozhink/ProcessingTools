namespace ProcessingTools.History.Data.Entity
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Entity;
    using ProcessingTools.History.Data.Entity.Contracts;
    using ProcessingTools.History.Data.Entity.Models;

    public class HistoryDbContext : EntityDbContext, IHistoryDbContext
    {
        public HistoryDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<ObjectHistory> ObjectHistories { get; set; }
    }
}
