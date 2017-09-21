namespace ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories
{
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBiotaxonomicBlackListRepository : ICrudRepository<IBlackListEntity>, IIterableRepository<IBlackListEntity>, IRepository<IBlackListEntity>
    {
    }
}
