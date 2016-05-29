namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class TaxonomicBlackListRepositoryProvider : ITaxonomicBlackListRepositoryProvider
    {
        public IGenericRepository<string> Create()
        {
            return new TaxonomicBlackListRepository();
        }
    }
}
