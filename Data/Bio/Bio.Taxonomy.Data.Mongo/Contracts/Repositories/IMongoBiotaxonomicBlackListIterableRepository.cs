namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Data.Common.Mongo.Contracts.Repositories;

    public interface IMongoBiotaxonomicBlackListIterableRepository : IMongoIterableRepository<IBlackListEntity>, IBiotaxonomicBlackListIterableRepository
    {
    }
}
