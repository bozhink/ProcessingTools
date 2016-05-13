namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface ITaxonomicBlackListRepositoryProvider : IGenericRepositoryProvider<IGenericRepository<string>, string>
    {
    }
}
