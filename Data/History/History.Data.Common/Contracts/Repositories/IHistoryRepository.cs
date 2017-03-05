namespace ProcessingTools.History.Data.Common.Contracts.Repositories
{
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IHistoryRepository : IAddableRepository<IHistoryItem>, IRemovableRepository<IHistoryItem>, ISavabaleRepository
    {
    }
}
