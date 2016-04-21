namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class TaxaRepositoryProvider : ITaxaRepositoryProvider
    {
        public IGenericRepository<Taxon> Create()
        {
            return new TaxaRepository(ConfigBuilder.Create());
        }
    }
}
