namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public interface IMongoBiotaxonomicBlackListRepository : IMongoCrudRepository<IBlackListEntity>, IBiotaxonomicBlackListRepository
    {
    }
}
