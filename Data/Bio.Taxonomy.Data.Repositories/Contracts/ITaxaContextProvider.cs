namespace ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Data.Common.Contracts;

    public interface ITaxaContextProvider : IDatabaseProvider<ITaxaContext>
    {
    }
}
