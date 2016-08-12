namespace ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IBioTaxonomyDataRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
