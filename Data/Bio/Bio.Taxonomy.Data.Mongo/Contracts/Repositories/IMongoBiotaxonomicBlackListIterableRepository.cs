namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Data.Common.Mongo.Repositories.Contracts;

    public interface IMongoBiotaxonomicBlackListIterableRepository : IMongoIterableRepository<IBlackListEntity>, IBiotaxonomicBlackListIterableRepository
    {
    }
}
