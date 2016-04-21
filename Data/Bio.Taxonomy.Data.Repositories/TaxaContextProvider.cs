namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;

    public class TaxaContextProvider : ITaxaContextProvider
    {
        public ITaxaContext Create()
        {
            return new TaxaContext();
        }
    }
}
