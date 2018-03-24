namespace ProcessingTools.History.Data.Entity.Contracts
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.History.Data.Entity.Models;

    public interface IHistoryDbContext : IDbContext
    {
        IDbSet<ObjectHistory> ObjectHistories { get; set; }
    }
}
