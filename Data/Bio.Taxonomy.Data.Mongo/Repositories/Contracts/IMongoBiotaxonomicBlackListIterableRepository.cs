namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories.Contracts;

    public interface IMongoBiotaxonomicBlackListIterableRepository : IMongoIterableRepository<IBlackListEntity>, IBiotaxonomicBlackListIterableRepository
    {
    }
}
