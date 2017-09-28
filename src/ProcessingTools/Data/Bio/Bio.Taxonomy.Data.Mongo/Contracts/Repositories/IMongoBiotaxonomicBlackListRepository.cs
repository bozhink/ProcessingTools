namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories.Bio.Taxonomy;
    using ProcessingTools.Data.Common.Mongo.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public interface IMongoBiotaxonomicBlackListRepository : IMongoCrudRepository<IBlackListEntity>, IBiotaxonomicBlackListRepository
    {
    }
}
