namespace ProcessingTools.Contracts.Data.History.Repositories
{
    using ProcessingTools.Contracts.Data.History.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IHistoryRepository : IAsyncRepository<IHistoryItem>
    {
    }
}
