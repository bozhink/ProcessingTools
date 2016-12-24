namespace ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories
{
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBiotaxonomicBlackListRepository : IAddableRepository<IBlackListEntity>, IDeletableRepository<IBlackListEntity>, IIterableRepository<IBlackListEntity>, IRepository<IBlackListEntity>, ISavabaleRepository
    {
    }
}
