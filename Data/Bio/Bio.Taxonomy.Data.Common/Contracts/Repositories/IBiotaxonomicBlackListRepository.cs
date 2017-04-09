namespace ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBiotaxonomicBlackListRepository : ICrudRepository<IBlackListEntity>, IIterableRepository<IBlackListEntity>, IRepository<IBlackListEntity>
    {
    }
}
