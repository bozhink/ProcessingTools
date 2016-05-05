namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface ITaxaRepositoryProvider : IGenericRepositoryProvider<IGenericRepository<Taxon>, Taxon>
    {
    }
}
