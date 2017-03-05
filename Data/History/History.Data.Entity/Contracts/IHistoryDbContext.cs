namespace ProcessingTools.History.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IHistoryDbContext : IDbContext
    {
        IDbSet<HistoryItem> HistoryItems { get; set; }
    }
}
