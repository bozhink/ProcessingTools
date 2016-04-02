namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class TaxonomicBlackListDataService : StringRepositoryDataServiceFactory, ITaxonomicBlackListDataService
    {
        public TaxonomicBlackListDataService(ITaxonomicBlackListRepository repository)
            : base(repository)
        {
        }
    }
}