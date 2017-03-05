namespace ProcessingTools.History.Data.Common.Contracts.Repositories
{
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IHistoryRepository : IFilterableRepository<IHistoryItem>, IAddableRepository<IHistoryItem>, IDeletableRepository<IHistoryItem>, ISavabaleRepository
    {
    }
}
